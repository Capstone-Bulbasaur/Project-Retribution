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
    public float fireRate = 1f;
    public float shootingRange;
    public GameObject IsarrProjectile;
    public GameObject IsarrProjectileParent;

    private float nextFireTime;
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
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange) 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer < shootingRange && nextFireTime < Time.time)
        {
            Instantiate(IsarrProjectile, IsarrProjectileParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    // Sphere draw for the Line of sight
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
