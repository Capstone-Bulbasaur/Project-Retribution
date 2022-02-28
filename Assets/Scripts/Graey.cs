using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graey : MonoBehaviour
{
    public float health;
    SpriteRenderer spriteRenderer;
    Rigidbody rigidbody;

    public void Attack()
    {
        
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
