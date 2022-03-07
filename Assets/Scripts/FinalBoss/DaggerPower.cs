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
            FireProjectile(Constants.Dagger, daggerProj);
            mouseFire = false;
        }

        if (controllerFire)
        {
            FireProjectile(Constants.Dagger, daggerProj);
            controllerFire = false;
        }

        if (phoneFire)
        {
            FireProjectile(Constants.Dagger, daggerProj);
            phoneFire = false;
        }
    }
}
