using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitcher : MonoBehaviour
{
    public static string activeWeaponType;


    public int powerUpTimeLimit = 15;
    public GameObject dagger;

    [Header("Water Power")] 
    public float waterPowerTimeLimit = 15f;
    public GameObject water;
    [Space]

    [Header("Fire Power")]
    public float firePowerTimeLimit = 15f;
    public GameObject fire;
    [Space]

    [Header("Water Power")]
    public float electricPowerTimeLimit = 15f;
    public GameObject electric;

    private GameObject activePower;
    // Start is called before the first frame update
    void Start()
    {
        activeWeaponType = Constants.Dagger;
        activePower = dagger;
    }

    IEnumerator LoadWeapons(GameObject weapon, float timeLimit)
    {
        dagger.SetActive(false);
        water.SetActive(false);
        fire.SetActive(false);
        electric.SetActive(false);

        weapon.SetActive(true);
        activePower = weapon;
        yield return new WaitForSeconds(timeLimit);

        loadDagger();
    }

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
        StopAllCoroutines();
        StartCoroutine(LoadWeapons(water, waterPowerTimeLimit));
    }

    private void PickupFirePower()
    {
        StopAllCoroutines();
        StartCoroutine(LoadWeapons(fire, firePowerTimeLimit));
    }

    private void PickupElectricPower()
    {
        StopAllCoroutines();
        StartCoroutine(LoadWeapons(electric, waterPowerTimeLimit));
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
