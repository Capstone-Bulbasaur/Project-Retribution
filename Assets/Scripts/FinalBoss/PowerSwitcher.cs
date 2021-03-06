using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSwitcher : MonoBehaviour
{
    public static string activeWeaponType;
    public HealthBar pickupTimer;
    private float timer;
    private float currentTime;
    public GameObject image;
    private Image pickupTimerImage;

    public GameObject dagger;
    public Sprite daggerSprite;


    [Header("Water Power")] 
    public float waterPowerTimeLimit = 15f;
    public GameObject water;
    public Sprite waterSprite;
    [Space]

    [Header("Fire Power")]
    public float firePowerTimeLimit = 15f;
    public GameObject fire;
    public Sprite fireSprite;
    [Space]

    [Header("Electric Power")]
    public float electricPowerTimeLimit = 15f;
    public GameObject electric;
    public Sprite electricSprite;


    private GameObject activePower;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        activeWeaponType = Constants.Dagger;
        activePower = dagger;

        audioManager = AudioManager.instance;

        pickupTimerImage = image.GetComponent<Image>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        
        //currentTime -= damages;
        if (timer <= 0)
        {
            pickupTimerImage.sprite = daggerSprite;
            pickupTimer.SetHealth(100);
        }
        else
        {
            pickupTimer.SetHealth(timer);
        }
        
    }

    IEnumerator LoadWeapons(GameObject weapon, float timeLimit)
    {
        timer = timeLimit;
        pickupTimer.SetMaxHealth((int)timer);
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

        if (audioManager.CheckIfPlaying("Boss_Flame"))
            audioManager.StopPlaying("Boss_Flame");

        if (audioManager.CheckIfPlaying("Boss_Water"))
            audioManager.StopPlaying("Boss_Water");
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
        StartCoroutine(LoadWeapons(electric, electricPowerTimeLimit));
    }

    public void PickupItem(int pickupType)
    {
        switch (pickupType)
        {
            case Constants.PickUpWater:
                pickupTimerImage.sprite = waterSprite;
                PickupWaterPower();
                break;
            case Constants.PickUpFire:
                pickupTimerImage.sprite = fireSprite;
                PickupFirePower();
                break;
            case Constants.PickUpElect:
                pickupTimerImage.sprite = electricSprite;
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
