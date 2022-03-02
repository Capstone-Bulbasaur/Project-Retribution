using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float range;
    private Vector3 shootDir;

    IEnumerator deathTimer()
    {
        //Destroy the object after its reached its max range
        yield return new WaitForSeconds(range);
        Destroy(transform.parent.gameObject);
    }

    public void Setup(Vector3 shootDirect, float projRange)
    {
        //Assign the Projectiles position the same as the shooting position
        this.shootDir = shootDirect;
        Debug.Log(shootDirect);
        //change the rotation of the object so that it faces the shooting direction
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));

        range = projRange;
        StartCoroutine(deathTimer());
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 10f;
        //Move the projectile towards the shooting direction
        transform.position += shootDir * (moveSpeed * Time.deltaTime);
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

    ////Destroy projectile once it is out of frame
    //private void OnBecameInvisible()
    //{
    //    Destroy(transform.parent.gameObject);
    //}

    //Destroy Projectile when it collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Projectile Collided with " + collision.gameObject);
        Destroy(transform.parent.gameObject);
    }
}
