using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrationManager : MonoBehaviour
{
    public bool savedIsarr;
    public GameObject Isarr;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SavedIsarr"))
        {
            savedIsarr = (PlayerPrefs.GetInt("SavedIsarr") != 0);
        }

        if (savedIsarr == true)
        {
            Isarr.SetActive(true);
        }
        else
        {
            Isarr.SetActive(false);
        }
    }
}
