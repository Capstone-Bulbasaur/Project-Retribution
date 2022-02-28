using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was inspired by https://www.youtube.com/watch?v=ebBxpwh3NvQ&ab_channel=gamesplusjames

public class DetectControlMethod : MonoBehaviour
{
    private PlayerAim player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {
        //Detect Mouse Input
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            player.useController = false;

        if (Input.GetAxisRaw("Mouse X") != 0.0f || Input.GetAxisRaw("Mouse Y") != 0)
            player.useController = false;

        if (player.rightStickInput.magnitude > 0.15)
            player.useController = true;
    }
}
