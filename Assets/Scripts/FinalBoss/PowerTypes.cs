using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PowerTypes : MonoBehaviour
{
    public float fireRate;

    protected float lastFireRate;


    // Start is called before the first frame update
    void Start()
    {
        lastFireRate = Time.time - 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Fire()
    {
        //Ray2D ray = 
    }
}
