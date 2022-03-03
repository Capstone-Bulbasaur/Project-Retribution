using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public int damage = 1;
    //Destroy Projectile when it collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(transform.parent.gameObject);
            //Debug.Log("Minion Collided with " + collision.gameObject);
            collision.gameObject.GetComponent<Graey>().TakeDamage(damage);
            
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player") )
    //    {
    //        Debug.Log("Minion Collided with " + collision.gameObject);
    //        Destroy(transform.parent.gameObject);
    //    }
    //}
}
