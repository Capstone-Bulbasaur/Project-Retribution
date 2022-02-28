using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitcher : MonoBehaviour
{
    public static string activeWeaponType;

    public GameObject dagger;
    public GameObject water;
    public GameObject fire;
    public GameObject electric;

    private GameObject activePower;
    // Start is called before the first frame update
    void Start()
    {
        activeWeaponType = Constants.Dagger;
        activePower = dagger;
    }

    public void loadWeapon(GameObject weapon)
    {
        dagger.SetActive(false);
        water.SetActive(false);
        fire.SetActive(false);
        electric.SetActive(false);

        weapon.SetActive(true);
        activePower = weapon;
    }

    private void PickupWaterPower()
    {
        loadWeapon(water);
    }

    IEnumerator loadWeapons(GameObject weapon)
    {
        yield return new WaitForSeconds(20);
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
