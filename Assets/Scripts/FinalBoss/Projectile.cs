using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float range;
    private Vector3 shootDir;
    private float shootSpeed;
    private int damage = 1;

    IEnumerator deathTimer()
    {
        //Destroy the object after its reached its max range
        yield return new WaitForSeconds(range);
        Disable();
    }

    public void Setup(Vector3 shootDirect, float projRange, int projDamage, float projSpeed)
    {
        //Assign the Projectiles position the same as the shooting position
        shootDir = shootDirect;
        //change the rotation of the object so that it faces the shooting direction
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));

        range = projRange;
        damage = projDamage;
        shootSpeed = projSpeed;

        StartCoroutine(deathTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.gameObject.activeInHierarchy)
        {
            //Move the projectile towards the shooting direction
            transform.position += shootDir * (shootSpeed * Time.deltaTime);
        }
    }

    //Calculate the angle the object needs to face
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        //Minus 90 because the sprite is originally at the 90 degree position instead of 0 degree
        return n-90;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<Graey>().TakeDamage(damage);
            Disable();
        }
        else if (collider.gameObject.CompareTag("Isarr") && this.gameObject.name != "IsarrBaseAttack-Sheet_0")
        {
            collider.gameObject.GetComponent<Enemy_Isarr>().TakeDamage(damage);
            Disable();
        }
        else if (collider.gameObject.CompareTag("NPC") && this.gameObject.name != "IsarrBaseAttack-Sheet_0")
        { 
            collider.transform.parent.gameObject.SetActive(false);

            Disable();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //On projectile collision with obstacles, disable projectile 
        if (other.gameObject.layer == 8)
        {
            Disable();
        }
    }

    private void Disable()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        //Reset teh position of the prefab
        transform.parent.position = Vector3.zero;
        gameObject.transform.position = Vector3.zero;
       
    }
}
