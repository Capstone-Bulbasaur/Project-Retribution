using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitcher : MonoBehaviour
{
    public static string activeWeaponType;

    public GameObject water;
    public GameObject fire;
    public GameObject electric;

    private GameObject activePower;
    // Start is called before the first frame update
    void Start()
    {
        activeWeaponType = Constants.Water;
        activePower = water;
    }

    public void loadWeapon(GameObject weapon)
    {
        water.SetActive(false);
        fire.SetActive(false);
        electric.SetActive(false);

        weapon.SetActive(true);
        activePower = weapon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void PickupWaterPower()
    {
        loadWeapon(water);
    }

    private void PickupFirePower()
    {
        loadWeapon(fire);
    }

    private void PickupElectricPower()
    {
        loadWeapon(electric);
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

    public GameObject GetActiveWeapon()
    {
        return activePower;
    }
}
