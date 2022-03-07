using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : Gun
{
    [SerializeField] private Transform electricProj;
    override protected void Update()
    {
        base.Update();

        //if (!aim.usePhone && !aim.useController)
        //{
        //    if (Input.GetMouseButton(0) && Time.time - lastFireTime > fireRate)
        //    {
        //        lastFireTime = Time.time;
        //        FireProjectile(Constants.PickUpElect, electricProj);
        //    }
        //}

        //if (aim.useController && !aim.usePhone)
        //{
        //    if (aim.contRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
        //    {
        //        lastFireTime = Time.time;
        //        FireProjectile(Constants.PickUpElect, electricProj);
        //    }
        //}

        //if (aim.usePhone && !aim.useController)
        //{
        //    if (aim.phoneRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
        //    {
        //        lastFireTime = Time.time;
        //        FireProjectile(Constants.PickUpElect, electricProj);
        //    }
        //}

        if (mouseFire)
        {
            FireProjectile(Constants.Electricity, electricProj);
            mouseFire = false;
        }

        if (controllerFire)
        {
            FireProjectile(Constants.Electricity, electricProj);
            controllerFire = false;
        }

        if (phoneFire)
        {
            FireProjectile(Constants.Electricity, electricProj);
            phoneFire = false;
        }
    }
}
