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

    public float shootSpeed;
    public int damage = 1;

    IEnumerator deathTimer()
    {
        //Destroy the object after its reached its max range
        yield return new WaitForSeconds(range);
        Disable();
        //Destroy(transform.parent.gameObject);
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
        //Invoke(nameof(Disable), range);
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
            Debug.Log("Isarr does damage to graey!");
            collider.gameObject.GetComponent<Graey>().TakeDamage(damage);
            Disable();
            //Destroy(transform.parent.gameObject);
        }
        else if (collider.gameObject.CompareTag("Isarr") && this.gameObject.name != "IsarrBaseAttack-Sheet_0")
        {
            Debug.Log("Projectile Triggered collide with " + collider.gameObject);
            collider.gameObject.GetComponent<Enemy_Isarr>().TakeDamage(damage);
            Disable();
            //Destroy(transform.parent.gameObject);
        }
        else if (collider.gameObject.CompareTag("NPC") && this.gameObject.name != "IsarrBaseAttack-Sheet_0")
        {
           Destroy(collider.transform.parent.gameObject);
           Disable();
           //Destroy(transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Debug.Log("Projectile hit terrain!");
            Disable();
            //Destroy(transform.parent.gameObject);
        }
    }

    private void Disable()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        transform.parent.position = Vector3.zero;
        gameObject.transform.position = Vector3.zero;
       
    }
}
