using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerPower : Gun
{
    [SerializeField] private Transform daggerProj;
    override protected void Update()
    {
        base.Update();

        if (mouseFire)
        {
            FireProjectile(Constants.PickUpDagger, daggerProj);
            mouseFire = false;
        }

        if (controllerFire)
        {
            FireProjectile(Constants.PickUpDagger, daggerProj);
            controllerFire = false;
        }

        if (phoneFire)
        {
            FireProjectile(Constants.PickUpDagger, daggerProj);
            phoneFire = false;
        }
    }
}
