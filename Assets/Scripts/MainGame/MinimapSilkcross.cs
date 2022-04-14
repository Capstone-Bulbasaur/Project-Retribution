using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSilkcross : MonoBehaviour
{
    private MinimapPlayerLocation minimap;

    // Start is called before the first frame update
    void Start()
    {
        minimap = FindObjectOfType<MinimapPlayerLocation>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       minimap.ChangePlayerLocation((int)Constants.minimapTowns.SILKCROSS);
    }
}
