using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/* script created following this tutorial: https://www.youtube.com/watch?v=_Z1t7MNk0c4
  
 */
public class Enemy_Isarr : MonoBehaviour
{
    
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    //public float nearDistance;
    public float startTimeBtwShots;
    
    private float timeBtwShots;
    private float health;

    public GameObject projectile;
    public Transform player;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        // Isarr movements
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if(Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        // Isarr shoot
        if(timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        
    }


}
