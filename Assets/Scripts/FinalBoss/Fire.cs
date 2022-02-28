using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Gun
{
    [SerializeField] private Transform fireProj;
    override protected void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0) && Time.time - lastFireTime > fireRate)
        {
            lastFireTime = Time.time;
            FireProjectile(Constants.PickUpFire, fireProj);
        }

        if (aim.rightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
        {
            lastFireTime = Time.time;
            FireProjectile(Constants.PickUpFire, fireProj);
        }
    }
}
