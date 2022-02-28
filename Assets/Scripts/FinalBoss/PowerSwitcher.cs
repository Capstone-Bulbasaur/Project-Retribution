using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitcher : MonoBehaviour
{
    public static string activeWeaponType;

    public int PowerUpTimeLimit = 15;
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

    //public void loadWeapon(GameObject weapon)
    //{
    //    dagger.SetActive(false);
    //    water.SetActive(false);
    //    fire.SetActive(false);
    //    electric.SetActive(false);

    //    weapon.SetActive(true);
    //    activePower = weapon;
    //}

    public void loadDagger()
    {
        water.SetActive(false);
        fire.SetActive(false);
        electric.SetActive(false);

        dagger.SetActive(true);
        activePower = dagger;
    }

    private void PickupWaterPower()
    {
        StartCoroutine(LoadWeapons(water));
    }

    IEnumerator LoadWeapons(GameObject weapon)
    {
        dagger.SetActive(false);
        water.SetActive(false);
        fire.SetActive(false);
        electric.SetActive(false);

        weapon.SetActive(true);
        activePower = weapon;
        yield return new WaitForSeconds(PowerUpTimeLimit);

        loadDagger();
    }

    private void PickupFirePower()
    {
        StartCoroutine(LoadWeapons(fire));
    }

    private void PickupElectricPower()
    {
        StartCoroutine(LoadWeapons(electric));
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
