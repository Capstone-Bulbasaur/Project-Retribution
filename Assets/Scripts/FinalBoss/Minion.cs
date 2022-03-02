using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
 
    //Destroy Projectile when it collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Minion Collided with " + collision.gameObject);
        Destroy(transform.parent.gameObject);
    }
}
