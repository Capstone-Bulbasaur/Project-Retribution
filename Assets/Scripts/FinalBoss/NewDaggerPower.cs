using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDaggerPower : Gun
{
    [SerializeField] private GameObject daggerPrefab;
    override protected void Update()
    {
        base.Update();

        if (mouseFire)
        {
            FireProjectile("Dagger", Constants.PickUpDagger, daggerPrefab);
            mouseFire = false;
        }

        if (controllerFire)
        {
            FireProjectile(Constants.Water, Constants.PickUpWater);
            controllerFire = false;
        }

        if (phoneFire)
        {
            FireProjectile(Constants.Water, Constants.PickUpWater);
            phoneFire = false;
        }
    }
}
