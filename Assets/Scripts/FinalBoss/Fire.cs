using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Gun
{
    override protected void Update()
    {
        base.Update();

        if (mouseFire)
        {
            FireProjectile(Constants.Fire, Constants.PickUpFire);
            mouseFire = false;
        }

        if (controllerFire)
        {
            FireProjectile(Constants.Fire, Constants.PickUpFire);
            controllerFire = false;
        }

        if (phoneFire)
        {
            FireProjectile(Constants.Fire, Constants.PickUpFire);
            phoneFire = false;
        }
    }
}
