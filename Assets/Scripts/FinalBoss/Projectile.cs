using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 shootDir;

    public void Setup(Vector3 shootDir)
    {
        //Assign the Projectiles position the same as the shooting position
        this.shootDir = shootDir;
        //change the rotation of the object so that it faces the shooting direction
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 10f;
        //Move the projectile towards the shooting direction
        transform.position += shootDir * (moveSpeed * Time.deltaTime);
    }

    protected void Fire()
    {
        //Ray2D ray = 
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

    //Destroy projectile once it is out of frame
    private void OnBecameInvisible()
    {
        Destroy(transform.parent.gameObject);
    }

    //Destroy Projectile when it collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(transform.parent.gameObject);
    }
}
