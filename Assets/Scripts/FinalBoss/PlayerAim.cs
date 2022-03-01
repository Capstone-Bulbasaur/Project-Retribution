using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class PlayerAim : MonoBehaviour
{
    public Vector3 shootPosition;
    public Vector3 shootDirection;
    public Transform aimTransform;
    public Vector2 contRightStickInput;
    public Vector2 phoneRightStickInput;
    public Joystick phoneRightStick;

    public DetectControlMethod controlMethod;
    [SerializeField] private GameObject rightStick;

    private void Start()
    {
        controlMethod = GetComponentInParent<DetectControlMethod>();
    }

    void Update()
    {
        contRightStickInput = new Vector2(Input.GetAxis("R_Horizontal"), Input.GetAxis("R_Vertical"));
        phoneRightStickInput = new Vector2(phoneRightStick.Horizontal, phoneRightStick.Vertical);

#if UNITY_ANDROID
        
        rightStick.SetActive(true);
        HandlePhoneAim();
#else
        if (!controlMethod.useController)
        {
            rightStick.SetActive(false);
            HandleMouseAim();
        }
        else if (controlMethod.useController)
        {
            rightStick.SetActive(false);
            HandleControllerAim();
        }
#endif    
    }


    void HandlePhoneAim()
    {
        if (phoneRightStickInput.magnitude > 0)
        {
            shootDirection = Vector3.left * (phoneRightStickInput.x * -1) + Vector3.up * phoneRightStickInput.y;
            //Quaternion playerRotation = Quaternion.LookRotation(curRotation, Vector3.forward);
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    void HandleControllerAim()
    {
        if (contRightStickInput.magnitude > 0)
        {
            shootDirection = Vector3.left * contRightStickInput.x + Vector3.up * contRightStickInput.y;
            //Quaternion playerRotation = Quaternion.LookRotation(curRotation, Vector3.forward);
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    /* Handle Mouse Aim functions were taken from */

    void HandleMouseAim()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        shootDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
