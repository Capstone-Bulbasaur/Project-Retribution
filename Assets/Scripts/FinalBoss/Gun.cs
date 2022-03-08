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

    private Vector3 projectileSpawnLocation;
    private float lastFireTime;
    private ProjectilePooler projectilePoller;

    protected bool mouseFire;
    protected bool controllerFire;
    protected bool phoneFire;

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

        projectilePoller = ProjectilePooler.Instance;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        projectileSpawnLocation = transform.position;

#if UNITY_ANDROID
        if (aim.phoneRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
        {
            lastFireTime = Time.time;
            phoneFire = true;
        }
#else
        if (!aim.controlMethod.useController)
        {
            if (Input.GetMouseButton(0) && Time.time - lastFireTime > fireRate)
            {
                lastFireTime = Time.time;
                mouseFire = true;
            }
        }
        else
        {
            if (aim.contRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
            {
                lastFireTime = Time.time;
                controllerFire = true;
            }
        }
#endif
        
    }

    protected virtual void FireProjectile(string type, int power)
    {
        //TODO ADD SOUND HERE

        Transform projectileTransform = null;

        //Check which power the Player has
        switch (power)
        {
            case Constants.PickUpDagger:
                    
                break;
            case Constants.PickUpWater:
                    
                break;
            case Constants.PickUpFire:
               
                //FindObjectOfType<AudioManager>().Play("Boss_Fireball");
                break;
            case Constants.PickUpElect:
               
                break;
            default:
                Debug.LogError("Bad pickup type passed" + type);
                break;
        }

        projectileTransform = projectilePoller.SpawnFromPool(type, shootPosition.position, Quaternion.identity);

        if (projectileTransform != null)
        {
            //Calculate the shoot direction with the mouse position and the projectile spawn position
            var shootDir = aim.shootDirection.normalized;
            //Send the shoot direction to the Projectiles script
            projectileTransform.GetComponentInChildren<Projectile>().Setup(shootDir, range, damage, projectileSpeed);
        }
    }
}
