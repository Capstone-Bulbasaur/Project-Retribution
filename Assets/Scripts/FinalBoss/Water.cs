using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Gun
{
    [SerializeField] private Transform waterProj;
    override protected void Update()
    {
        base.Update();

        //if (!aim.usePhone && !aim.useController)
        //{
        //    if (Input.GetMouseButton(0) && Time.time - lastFireTime > fireRate)
        //    {
        //        lastFireTime = Time.time;
        //        FireProjectile(Constants.PickUpWater, waterProj);
        //    }
        //}

        //if (aim.useController && !aim.usePhone)
        //{
        //    if (aim.contRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
        //    {
        //        lastFireTime = Time.time;
        //        FireProjectile(Constants.PickUpWater, waterProj);
        //    }
        //}

        //if (aim.usePhone && !aim.useController)
        //{
        //    if (aim.phoneRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
        //    {
        //        lastFireTime = Time.time;
        //        FireProjectile(Constants.PickUpWater, waterProj);
        //    }
        //}

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
