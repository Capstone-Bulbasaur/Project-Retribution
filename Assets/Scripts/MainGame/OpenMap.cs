using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap : MonoBehaviour
{
    public GameObject transparency;
    public GameObject map;
    bool active;

    public void OpenCloseMap()
    {
        if(active == false)
        {
            transparency.transform.gameObject.SetActive(true);
            map.transform.gameObject.SetActive(true);
            active = true;
        }
        else
        {
            transparency.transform.gameObject.SetActive(false);
            map.transform.gameObject.SetActive(false);
            active = false;
        }
    }
}
