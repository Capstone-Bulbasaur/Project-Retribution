using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MinimapPlayerLocation : MonoBehaviour
{
    [SerializeField] private List<GameObject> locations;

    public void ChangePlayerLocation(int location)
    {
        foreach (GameObject obj in locations)
        {
            obj.SetActive(false);
        }

        locations[location].SetActive(true);
    }
}
