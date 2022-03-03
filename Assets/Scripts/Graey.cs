using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graey : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    
    SpriteRenderer spriteRenderer;
    Rigidbody rigidbody;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // Health bar tester
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
        }
    }

    // Health bar tester
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    } 
}
