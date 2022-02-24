using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graey : MonoBehaviour
{
    public float health;
    SpriteRenderer spriteRenderer;
    Rigidbody rigidbody;

    private int activePower = 0;

    public void Attack()
    { 
        
    }

    private void PickupWaterPower()
    {
        activePower = 1;
    }

    private void PickupFirePower()
    {
        activePower = 2;
    }

    private void PickupElectricPower()
    {
        activePower = 3;
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
