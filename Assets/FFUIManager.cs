using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameObject = UnityEngine.GameObject;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using TMPro;

public class FFUIManager : MonoBehaviour
{
    //I won't lie team, beyond adding a timer, I don't know what this Manager is for.
    //ha, called it. To credit accordingly: Based on timer logic used in RestarauntRush
    // Primary game time set up
    public float CurrentTime;
    public float StartingTime = 100;
    public TextMeshProUGUI Timer;

    private bool isGameOver = false;
    private float WaitforInstructions = 6.0f; // TODO - fix this hardcoded after Level Up.

    void Awake()
    {
        CurrentTime = StartingTime;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!isGameOver)
        {
            WaitforInstructions -= Time.deltaTime;
            if (WaitforInstructions < 0)
            {
                if (CurrentTime <= 0)
                {
                    isGameOver = true;
                    return;
                }
                else
                {
                    CurrentTime -= 1 * Time.deltaTime;
                }
            }
            Timer.text = CurrentTime.ToString("0");
        }
    }
}
