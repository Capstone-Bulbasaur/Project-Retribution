using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Gun
{
    [SerializeField] private Transform waterProj;
    override protected void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile(Constants.PickUpWater, waterProj);
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            FireProjectile(Constants.PickUpWater, waterProj);
        }
    }
}
