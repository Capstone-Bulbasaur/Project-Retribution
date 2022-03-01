using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    public float fireRate;
    public float range;
    public int damage;
    [HideInInspector] public PlayerAim aim;

    [SerializeField] private Transform shootPosition;

    private Vector3 projectileSpawnLocation;
    private float lastFireTime;

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
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        projectileSpawnLocation = transform.position;

        if (!aim.controlMethod.usePhone && !aim.controlMethod.useController)
        {
            if (Input.GetMouseButton(0) && Time.time - lastFireTime > fireRate)
            {
                lastFireTime = Time.time;
                mouseFire = true;
            }
        }

        if (aim.controlMethod.useController && !aim.controlMethod.usePhone)
        {
            if (aim.contRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
            {
                lastFireTime = Time.time;
                controllerFire = true;
            }
        }

        if (aim.controlMethod.usePhone && !aim.controlMethod.useController)
        {
            if (aim.phoneRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
            {
                lastFireTime = Time.time;
                phoneFire = true;
            }
        }
    }

    protected virtual void FireProjectile(int type, Transform power)
    {
        //TODO ADD SOUND HERE

        Transform projectileTransform = null;

        //Check which power the Player has
        switch (type)
        {
            case Constants.PickUpDagger:
                projectileTransform = Instantiate(power, projectileSpawnLocation, Quaternion.identity, GameObject.Find("Projectiles_Blank").transform);
                break;
            case Constants.PickUpWater:
                projectileTransform = Instantiate(power, projectileSpawnLocation, Quaternion.identity, GameObject.Find("Projectiles_Blank").transform);
                break;
            case Constants.PickUpFire:
                projectileTransform = Instantiate(power, projectileSpawnLocation, Quaternion.identity, GameObject.Find("Projectiles_Blank").transform);
                //FindObjectOfType<AudioManager>().Play("Boss_Fireball");
                break;
            case Constants.PickUpElect:
                projectileTransform = Instantiate(power, projectileSpawnLocation, Quaternion.identity, GameObject.Find("Projectiles_Blank").transform);
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
            projectileTransform.GetComponentInChildren<Projectile>().Setup(shootDir, range);
        }
    }
}
