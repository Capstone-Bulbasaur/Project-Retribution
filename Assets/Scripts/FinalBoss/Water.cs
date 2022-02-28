using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Gun
{
    [SerializeField] private Transform waterProj;
    override protected void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0) && Time.time - lastFireTime > fireRate)
        {
            lastFireTime = Time.time;
            FireProjectile(Constants.PickUpWater, waterProj);
        }

        if (aim.rightStickInput.magnitude > 0.5 && Time.time - lastFireTime > fireRate)
        {
            lastFireTime = Time.time;
            FireProjectile(Constants.PickUpWater, waterProj);
        }
    }
}
