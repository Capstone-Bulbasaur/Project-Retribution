using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/* script created following this tutorial: https://www.youtube.com/watch?v=lHLZxd0O6XY
  
 */
public class Enemy_Isarr : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // set the player distance
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        // if the DistanceFromPlayer is < than LineOfSight, the Enemy will follow the player
        if (distanceFromPlayer < lineOfSight) 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
    }

    // Sphere draw for the Line of sight
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}
