using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RestaurantRush_GameMananger : MonoBehaviour
{
    // Primary game time set up
    public float CurrentTime;
    public float StartingTime = 100;
    // order time set up
    public float OrderStartTime = 10;
    public float OrderCurrentTime;


    public int maxNumCustomers;
    public int CurrentNumCustomers;

    
    void Start()
    {
        CurrentTime = StartingTime;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
