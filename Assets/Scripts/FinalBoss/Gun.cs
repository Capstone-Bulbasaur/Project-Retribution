using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class Gun : MonoBehaviour
{
    public float fireRate;
    public float projectileSpeed;
    public float range;
    public int damage;
    [HideInInspector] public PlayerAim aim;

    [SerializeField] private Transform shootPosition;

    //private Vector3 projectileSpawnLocation;
    protected float lastFireTime;
    private ProjectilePooler projectilePoller;
    private AudioManager audioManager;

    protected bool mouseFire;
    protected bool controllerFire;
    protected bool phoneFire;
    protected bool isFiring = false;

    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "FinalBoss")
        {
            GetComponent<Gun>().enabled = false;
            GetComponentInParent<PlayerAim>().enabled = false;
            GetComponentInParent<DetectControlMethod>().enabled = false;
            GetComponentInParent<Graey>().enabled = false;
        }

        aim = GetComponentInParent<PlayerAim>();
        lastFireTime = Time.time - 10;
        
        
    }

    private void Start()
    {
        projectilePoller = ProjectilePooler.Instance;
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //projectileSpawnLocation = transform.position;

#if UNITY_ANDROID
        if (aim.phoneRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
        {
            lastFireTime = Time.time;
            phoneFire = true;
        }

        if (aim.phoneRightStickInput.magnitude > 0.5)
        {
            isFiring = false;
        }
#else
        if (!aim.controlMethod.useController)
        {
            if (Input.GetMouseButton(0) && Time.time - lastFireTime > fireRate)
            {
                lastFireTime = Time.time;
                mouseFire = true;
                isFiring = true;
            }
        }
        else
        {
            if (aim.contRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
            {
                lastFireTime = Time.time;
                controllerFire = true;
                isFiring = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
        }
#endif
        if (!isFiring)
        {
            if (audioManager.CheckIfPlaying("Boss_Flame"))
                audioManager.StopPlaying("Boss_Flame");

            if (audioManager.CheckIfPlaying("Boss_Water"))
                audioManager.StopPlaying("Boss_Water");

            //if (audioManager.CheckIfPlaying("Boss_Water"))
            //    audioManager.StopPlaying("Boss_Water");
        }
    }

    protected virtual void FireProjectile(string type, int power)
    {
        //TODO ADD SOUND HERE

        GameObject projectileTransform = null;

        //Check which power the Player has
        switch (power)
        {
            case Constants.PickUpDagger:
                //TODO SOUND
                if (!audioManager.CheckIfPlaying("Boss_Dagger"))
                    audioManager.Play("Boss_Dagger");
                break;
            case Constants.PickUpWater:
                //TODO ADD SOUND HERE
                if (!audioManager.CheckIfPlaying("Boss_Water"))
                    audioManager.Play("Boss_Water");
                break;
            case Constants.PickUpFire:
                //TODO ADD SOUND HERE
                if (!audioManager.CheckIfPlaying("Boss_Flame"))
                    audioManager.Play("Boss_Flame");
                break;
            case Constants.PickUpElect:
                //TODO ADD SOUND HERE
                if (!audioManager.CheckIfPlaying("Boss_Electric"))
                    audioManager.Play("Boss_Electric");
                break;
            default:
                Debug.LogError("Bad pickup type passed" + type);
                break;
        }

        projectileTransform = projectilePoller.SpawnFromPool(type, shootPosition.position);

        if (projectileTransform != null)
        {
            //Calculate the shoot direction with the mouse position and the projectile spawn position
            var shootDir = aim.shootDirection.normalized;
            //Send the shoot direction to the Projectiles script
            projectileTransform.GetComponentInChildren<Projectile>().Setup(shootDir, range, damage, projectileSpeed);
        }
    }
}
