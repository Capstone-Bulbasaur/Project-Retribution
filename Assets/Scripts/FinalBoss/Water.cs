using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Gun
{
    override protected void Update()
    {
        base.Update();

        if (mouseFire)
        {
            FireProjectile(Constants.Water, Constants.PickUpWater);
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
