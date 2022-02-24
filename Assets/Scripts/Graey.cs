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

    private void PickupFirePower()
    {
       
    }

    private void PickupWaterPower()
    {
       
    }

    private void PickupElectricPower()
    {
       
    }

    public void PickupItem(int pickupType)
    {
        switch (pickupType)
        {
            case Constants.PickUpFire:
                PickupFirePower();
                break;
            case Constants.PickUpWater:
                PickupWaterPower();
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
