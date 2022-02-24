using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform waterProj;
    [SerializeField] private Transform fireProj;
    [SerializeField] private Transform electricProj;

    [SerializeField] private Transform shootPosition;

    private PlayerAim aim;
    private Vector3 projectileSpawnLocation;

    public KeyCode actionKey; //Testing purpose
    public KeyCode actionKey1; //Testing purpose
    public KeyCode actionKey2; //Testing purpose

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "FinalBoss")
        {
            GetComponent<Gun>().enabled = false;
            GetComponent<PlayerAim>().enabled = false;
        }

        aim = GetComponentInParent<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {
        projectileSpawnLocation = transform.position;
        if(Input.GetKeyDown(actionKey))
        {
           FireProjectile(Constants.PickUpWater);

        }
        else if (Input.GetKeyDown(actionKey1))
        {
            FireProjectile(Constants.PickUpFire);
        }
        else if (Input.GetKeyDown(actionKey2))
        {
            FireProjectile(Constants.PickUpElect);
        }

        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile(Constants.PickUpWater);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            FireProjectile(Constants.PickUpFire);
        }
    }

    public void FireProjectile(int type)
    {
        //TODO ADD SOUND HERE

        Transform projectileTransform;
        Vector3 shootDir;
        switch (type)
        {
            case Constants.PickUpWater:
                projectileTransform = Instantiate(waterProj, projectileSpawnLocation, Quaternion.identity);

                shootDir = (aim.shootPosition - projectileSpawnLocation).normalized;
                projectileTransform.GetComponentInChildren<Projectile>().Setup(shootDir);
                break;
            case Constants.PickUpFire:
                projectileTransform = Instantiate(fireProj, projectileSpawnLocation, Quaternion.identity);
                shootDir = (aim.shootPosition - projectileSpawnLocation).normalized;
                projectileTransform.GetComponentInChildren<Projectile>().Setup(shootDir);
                break;
            case Constants.PickUpElect:
                projectileTransform = Instantiate(electricProj, projectileSpawnLocation, Quaternion.identity);
                shootDir = (aim.shootPosition - projectileSpawnLocation).normalized;
                projectileTransform.GetComponentInChildren<Projectile>().Setup(shootDir);
                break;
            default:
                Debug.LogError("Bad pickup type passed" + type);
                break;
        }
    }
}
