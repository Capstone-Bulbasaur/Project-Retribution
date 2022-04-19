using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

public class Minion : MonoBehaviour
{
    private FBGameManager gameManager;

    public int damage = 1;
    public GameObject particleObject;

    private void Start()
    {
        gameManager = FBGameManager.instance;
    }

    //Destroy Projectile when it collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DisableMinion();

            collision.gameObject.GetComponent<Graey>().TakeDamage(damage);
        }
    }

    private void Update()
    {
        if (gameManager.isGameOver)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void DisableMinion()
    {
        if (gameObject.transform.position != Vector3.zero)
        {
            //Play particle effect 
            Instantiate(particleObject, gameObject.transform.position, Quaternion.identity);
        }
        
        if (ProjectilePooler.Instance.isLoaded)
        {
            //Reset game object positions
            transform.parent.position = Vector3.zero;
            gameObject.transform.position = Vector3.zero;

            ProjectilePooler.Instance.PutInQueue(gameObject.transform.parent.gameObject);

            //Disable minion
            transform.parent.gameObject.SetActive(false);
            
        }
    }

    public void EnableMinion()
    {
        if (ProjectilePooler.Instance.isLoaded)
        {
            transform.parent.gameObject.SetActive(true);
            gameObject.transform.localPosition = Vector3.zero;
        }
    }
}
