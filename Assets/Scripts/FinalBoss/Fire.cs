using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Gun
{
    [SerializeField] private Transform fireProj;
    override protected void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile(Constants.PickUpFire, fireProj);
        }
    }
}
