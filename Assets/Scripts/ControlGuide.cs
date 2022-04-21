using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGuide : MonoBehaviour
{
    public Canvas pcControls;
    public Canvas mobileControls;

    void Awake()
    {
        pcControls.enabled = false;
        mobileControls.enabled = false;
    }
    void Start()
    {
#if UNITY_ANDROID
        mobileControls.enabled = true;
#else
        pcControls.enabled = true;
#endif
    }
}
