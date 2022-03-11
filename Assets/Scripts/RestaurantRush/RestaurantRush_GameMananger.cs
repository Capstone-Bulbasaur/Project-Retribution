using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RestaurantRush_GameMananger : MonoBehaviour
{
    // Primary game time set up
     float CurrentTime;
    public float StartingTime = 100;
    // order time set up
    public float OrderStartTime = 10;
     float OrderCurrentTime;


    public int maxNumCustomers;
     int CurrentNumCustomers;


   // public TextMeshProUGUI MainGameTimer;

    void Start()
    {
        CurrentTime = StartingTime;

    }
    // Update is called once per frame
    void Update()
    {
        CurrentTime -= 1 * Time.deltaTime;

        if (CurrentTime != 0)
        {
            print(CurrentTime);
        }
        if (CurrentTime <= 0)
        {
            CurrentTime = 0;
        }
    }
}
