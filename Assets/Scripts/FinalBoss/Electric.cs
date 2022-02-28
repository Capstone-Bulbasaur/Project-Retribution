using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : Gun
{
    [SerializeField] private Transform electricProj;
    override protected void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0) && Time.time - lastFireTime > fireRate)
        {
            lastFireTime = Time.time;
            FireProjectile(Constants.PickUpElect, electricProj);
        }

        if (aim.rightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
        {
            lastFireTime = Time.time;
            FireProjectile(Constants.PickUpElect, electricProj);
        }
    }
}
