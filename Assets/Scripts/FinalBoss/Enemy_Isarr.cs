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
    public float projectileSpeed;
    public int damage = 1;
    public Transform projectile;
    public Transform player;
    public float projRange;
    
    private float timeBtwShots;
    private float health;
    private ProjectilePooler projectilePoller;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private SpriteRenderer spriteRenderer;
    private Rigidbody rigidbody;
    private FBGameManager gameManager;
    [SerializeField] private Color hitColor;


    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform; not using for now. Changed player to public above, and assigned manually the player on the inspector
        timeBtwShots = startTimeBtwShots;
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

        projectilePoller = ProjectilePooler.Instance;

        spriteRenderer = GetComponent<SpriteRenderer>();

        gameManager = FBGameManager.instance;
    }

    void Update()
    {
        if (gameManager.isGameOver)
        {
            return;
        }

        if (currentHealth > 0)
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
                GameObject projectileTransform = null;
                var shootDir = player.transform.position - gameObject.transform.position;
                //projectileTransform = Instantiate(projectile, transform.position, Quaternion.identity);
                projectileTransform = projectilePoller.SpawnFromPool("Isarr", transform.position);
                projectileTransform.GetComponentInChildren<Projectile>().Setup(shootDir.normalized, projRange, damage, projectileSpeed);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        else
        {
            //Move Isarr to the middle of the field
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(1, -5), speed * Time.deltaTime);
        }
    }

    // Health bar 
    public void TakeDamage(int damages)
    {
        currentHealth -= damages; 
        healthBar.SetHealth(currentHealth);
        AudioManager.instance.Play("Boss_IsarrHit");

        spriteRenderer.color = hitColor;
        StartCoroutine(ChangeSpriteColor());

        if (currentHealth <= 0)
        {
            AudioManager.instance.StopPlaying("Boss_Music");
        }
    }

    IEnumerator ChangeSpriteColor()
    {
        yield return new WaitForSeconds(.5f);
        spriteRenderer.color = Color.white;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

}
