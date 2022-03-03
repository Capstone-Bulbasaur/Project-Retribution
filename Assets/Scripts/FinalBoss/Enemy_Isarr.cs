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
    public float startTimeBtwShots;
    public Transform projectile;
    public Transform player;
    public float projRange;
    
    private float timeBtwShots;
    private float health;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    SpriteRenderer spriteRenderer;
    Rigidbody rigidbody;


    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform; not using for now. Changed player to public above, and assigned manually the player on the inspector
        timeBtwShots = startTimeBtwShots;
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
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
            Transform projectileTransform = null;
            var shootDir = player.transform.position - gameObject.transform.position;
            projectileTransform = Instantiate(projectile, transform.position, Quaternion.identity);
            projectileTransform.GetComponentInChildren<Projectile>().Setup(shootDir.normalized, projRange);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        // Health bar tester
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
        }
    }

    // Health bar 
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            FindObjectOfType<AudioManager>().StopPlaying("Boss_Music");
            FindObjectOfType<AudioManager>().Play("Boss_IsarrDeath");
        }
    }

}
