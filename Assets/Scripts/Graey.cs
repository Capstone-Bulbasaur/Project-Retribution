using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graey : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public Animator camAnim;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody rigidbody;
    [SerializeField] private Color hitColor;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Health bar tester
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        AudioManager.instance.Play("Boss_GraeyHurt");

        spriteRenderer.color = hitColor;
        StartCoroutine(ChangeSpriteColor());

        CamShake();

        if (currentHealth <= 0)
        {
            PlayerDeath.instance.EnableObject();
            PlayerDeath.instance.FadeToRestart();
            FBGameManager.instance.isGameOver = true;
        }
    }

    IEnumerator ChangeSpriteColor()
    {
        yield return new WaitForSeconds(.5f);
        spriteRenderer.color = Color.white;
    }

    public void CamShake()
    {
        camAnim.SetTrigger("shake");
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
