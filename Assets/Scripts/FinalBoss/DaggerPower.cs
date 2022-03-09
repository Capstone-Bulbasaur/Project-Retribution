using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerPower : Gun
{
    override protected void Update()
    {
        base.Update();

        if (mouseFire)
        {
            FireProjectile(Constants.Dagger, Constants.PickUpDagger);
            mouseFire = false;
        }

        if (controllerFire)
        {
            FireProjectile(Constants.Dagger, Constants.PickUpDagger);
            controllerFire = false;
        }

        if (phoneFire)
        {
            FireProjectile(Constants.Dagger, Constants.PickUpDagger);
            phoneFire = false;
        }
    }
}
