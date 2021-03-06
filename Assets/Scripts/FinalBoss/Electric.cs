using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : Gun
{
    [SerializeField] private Transform electricProj;
    override protected void Update()
    {
        base.Update();

        if (mouseFire)
        {
            FireProjectile(Constants.Electricity, Constants.PickUpElect);
            mouseFire = false;
        }

        if (controllerFire)
        {
            FireProjectile(Constants.Electricity, Constants.PickUpElect);
            controllerFire = false;
        }

        if (phoneFire)
        {
            FireProjectile(Constants.Electricity, Constants.PickUpElect);
            phoneFire = false;
        }
    }
}
