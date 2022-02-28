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

    private PowerSwitcher powerSwitcher;

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
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    public void Attack()
    {
        powerSwitcher = GetComponent<PowerSwitcher>();
    }



    private void PickupWaterPower()
    {
       //powerSwitcher.loadWeapon(GameObject.Find("Water"));
    }

    private void PickupFirePower()
    {
        //powerSwitcher.loadWeapon(GameObject.Find(Constants.Fire));
    }

    private void PickupElectricPower()
    {
        //powerSwitcher.loadWeapon(GameObject.Find("Electric"));
    }

    public void PickupItem(int pickupType)
    {
        switch (pickupType)
        {
            case Constants.PickUpWater:
                PickupWaterPower();
                break;
            case Constants.PickUpFire:
                PickupFirePower();
                break;
            case Constants.PickUpElect:
                PickupElectricPower();
                break;
            default:
                Debug.LogError("Bad pickup type passed" + pickupType);
                break;
        }
    }
}
