using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* [MS] LineOfSight and shooting part of the script created following this tutorial: https://www.youtube.com/watch?v=lHLZxd0O6XY
  
 */

public class Isarr : MonoBehaviour
{
    
    public float health;
    public float fireRate;
    private float nextFireTime;
    public float speed;
    public float shootingRange;
    public GameObject IsarrProjectile;
    public GameObject IsarrProjectileParent;
    public Animator animator;
    public GameObject target;

    public float lineOfSight;
    private Transform player;

    //[MS] - double check later if we really need, as we already have the general SoundManager
    private AudioSource audioSource;

    void Start()
    {
        //animator = GetComponent<Animator>();
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
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
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
