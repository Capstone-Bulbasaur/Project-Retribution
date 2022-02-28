using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    public float fireRate;
    public int range;
    public int damage;
    [HideInInspector] public PlayerAim aim;

    [SerializeField] private Transform shootPosition;

    private Vector3 projectileSpawnLocation;

    protected float lastFireTime; 

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

    // Update is called once per frame
    protected virtual void Update()
    {
        projectileSpawnLocation = transform.position;
    }

    protected virtual void FireProjectile(int type, Transform power)
    {
        //TODO ADD SOUND HERE

        Transform projectileTransform = null;

        //Check which power the Player has
        switch (type)
        {
            case Constants.PickUpDagger:
                projectileTransform = Instantiate(power, projectileSpawnLocation, Quaternion.identity);
                break;
            case Constants.PickUpWater:
                projectileTransform = Instantiate(power, projectileSpawnLocation, Quaternion.identity);
                break;
            case Constants.PickUpFire:
                projectileTransform = Instantiate(power, projectileSpawnLocation, Quaternion.identity);
                //FindObjectOfType<AudioManager>().Play("Boss_Fireball");
                break;
            case Constants.PickUpElect:
                projectileTransform = Instantiate(power, projectileSpawnLocation, Quaternion.identity);
                break;
            default:
                Debug.LogError("Bad pickup type passed" + type);
                break;
        }

        if (projectileTransform != null)
        {
            //Calculate the shoot direction with the mouse position and the projectile spawn position
            var shootDir = aim.shootDirection.normalized;
            //Send the shoot direction to the Projectiles script
            projectileTransform.GetComponentInChildren<Projectile>().Setup(shootDir);
        }
    }
}
