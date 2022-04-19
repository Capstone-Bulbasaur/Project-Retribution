using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGuide : MonoBehaviour
{
    public GameObject movePC;
    public GameObject moveMobile;
    public GameObject speakMobile;

    void Awake()
    {
        movePC.SetActive(false);
        moveMobile.SetActive(false);
    }
    void Start()
    {
#if UNITY_ANDROID
        moveMobile.SetActive(true);
#else
        movePC.SetActive(true);
#endif
    }
}
