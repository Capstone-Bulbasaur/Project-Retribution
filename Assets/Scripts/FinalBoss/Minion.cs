using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minion : MonoBehaviour
{
    private FBGameManager gameManager;
    public int damage = 1;

    private void Start()
    {
        gameManager = FBGameManager.instance;
    }

    //Destroy Projectile when it collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Destroy(transform.parent.gameObject);
            transform.parent.gameObject.SetActive(false);
           
            //Debug.Log("Minion Collided with " + collision.gameObject);
            collision.gameObject.GetComponent<Graey>().TakeDamage(damage);
        }
    }

    private void Update()
    {
        if (gameManager.CheckGameOver())
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (ProjectilePooler.Instance.isLoaded)
        {
            //Remove enemy from onScreen variable
            gameManager.RemoveEnemy();
            transform.parent.position = Vector3.zero;
            gameObject.transform.position = Vector3.zero;
        }
    }
}
