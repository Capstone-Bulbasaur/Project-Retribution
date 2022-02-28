using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was inspired by https://www.youtube.com/watch?v=ebBxpwh3NvQ&ab_channel=gamesplusjames

public class DetectControlMethod : MonoBehaviour
{
    public bool useController;
    public bool usePhone;

    private PlayerAim player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInChildren<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            useController = false;
            usePhone = true;
        }
        else
        {
            //Detect Mouse Input
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                useController = false;
                usePhone = false;
            }
            //Detect Mouse Input
            if (Input.GetAxisRaw("Mouse X") != 0.0f || Input.GetAxisRaw("Mouse Y") != 0)
            {
                useController = false;
                usePhone = false;
            }
            //Detect Controller Input
            if (player.contRightStickInput.magnitude > 0.15)
            {
                useController = true;
                usePhone = false;
            }
        }
    }
}
