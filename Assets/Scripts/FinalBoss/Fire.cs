using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Gun
{
    [SerializeField] private Transform fireProj;
    override protected void Update()
    {
        base.Update();

        //if (!aim.usePhone && !aim.useController)
        //{
        //    if (Input.GetMouseButton(0) && Time.time - lastFireTime > fireRate)
        //    {
        //        lastFireTime = Time.time;
        //        FireProjectile(Constants.PickUpFire, fireProj);
        //    }
        //}

        //if (aim.useController && !aim.usePhone)
        //{
        //    if (aim.contRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
        //    {
        //        lastFireTime = Time.time;
        //        FireProjectile(Constants.PickUpFire, fireProj);
        //    }
        //}

        //if (aim.usePhone && !aim.useController)
        //{
        //    if (aim.phoneRightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
        //    {
        //        lastFireTime = Time.time;
        //        FireProjectile(Constants.PickUpFire, fireProj);
        //    }
        //}

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
