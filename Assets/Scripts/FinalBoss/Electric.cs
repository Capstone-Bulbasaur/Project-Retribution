using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : Gun
{
    [SerializeField] private Transform electricProj;
    override protected void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile(Constants.PickUpElect, electricProj);
        }
    }
}
