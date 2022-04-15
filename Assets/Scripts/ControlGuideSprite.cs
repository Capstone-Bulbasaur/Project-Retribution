using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGuideSprite : MonoBehaviour
{
    public GameObject move;
    public Canvas moveMobile;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        move.SetActive(false);
        moveMobile.enabled = true;
#else
        move.SetActive(true);
        moveMobile.enabled = false;
#endif
    }
}
