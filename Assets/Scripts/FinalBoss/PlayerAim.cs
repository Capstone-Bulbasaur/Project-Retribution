using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class PlayerAim : MonoBehaviour
{
    public Vector3 shootPosition;
    public Vector3 shootDirection;
    public Transform aimTransform;
    public bool useController;
    public Vector2 rightStickInput;

    void Update()
    {
        rightStickInput = new Vector2(Input.GetAxis("R_Horizontal"), Input.GetAxis("R_Vertical"));

        if (!useController)
        {
            HandleMouseAim();
        }
        else if (useController)
        {
            HandleControllerAim();
        }
    }

    void HandleControllerAim()
    {
        if (rightStickInput.magnitude > 0)
        {
            shootDirection = Vector3.left * rightStickInput.x + Vector3.up * rightStickInput.y;
            //Quaternion playerRotation = Quaternion.LookRotation(curRotation, Vector3.forward);
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    void HandleMouseAim()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        shootDirection = (mousePosition - transform.position).normalized;
        shootPosition = mousePosition;
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
