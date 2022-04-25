using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapLimecoast : MonoBehaviour
{
    private MinimapPlayerLocation minimap;

    // Start is called before the first frame update
    void Start()
    {
        minimap = FindObjectOfType<MinimapPlayerLocation>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       minimap.ChangePlayerLocation((int)Constants.minimapTowns.LIMECOAST);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        minimap.ChangePlayerLocation((int)Constants.minimapTowns.LIMECOAST);
    }
}
