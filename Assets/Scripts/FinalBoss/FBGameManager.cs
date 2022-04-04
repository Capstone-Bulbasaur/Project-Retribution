using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class FBGameManager : MonoBehaviour
{
    public static FBGameManager instance;

    public GameObject player;
    public GameObject minion;
    public GameObject[] spawnPoints;
    public int maxMinionsOnScreen;
    public int totalMinions;
    public int minionsPerSpawn;
    public float minSpawnTime; //UML has this as a float, anyone care?
    public float maxSpawnTime; //UML also says float
    public bool isGameOver = true;

    private int minionsOnScreen = 0;
    private float generatedSpawnTime = 0;
    private float currentSpawnTime = 0;
    private ProjectilePooler projectilePoller;
    
    
    private Enemy_Isarr isarr;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void PauseMenu()
    {
        //functionality to enter pause menu will go here
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("Boss_Music");
        projectilePoller = ProjectilePooler.Instance;

        isarr = FindObjectOfType<Enemy_Isarr>();

        Invoke("StartGame", 2.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (!isGameOver)
        {
            //Set game over if Isarr's health goes to 0 or below
            if (isarr.GetHealth() <= 0)
            {
                isGameOver = true;

                LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.FINALBOSSWIN);
            }
            else if (isarr.GetHealth() <= 50)
            {
                maxSpawnTime = 1.5f;
                minSpawnTime = 0.5f;
            }
            else if (isarr.GetHealth() <= 25)
            {
                maxSpawnTime = 0.5f;
                minSpawnTime = 0f;
            }

            //SPAWN MINIONS CODE
            currentSpawnTime += Time.deltaTime;

            if (currentSpawnTime > generatedSpawnTime)
            {
                currentSpawnTime = 0;
                generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

                if (minionsPerSpawn > 0 && minionsOnScreen < totalMinions)
                {
                    List<int> previousSpawnLocations = new List<int>();

                    if (minionsPerSpawn > spawnPoints.Length)
                    {
                        minionsPerSpawn = spawnPoints.Length - 1;
                    }

                    minionsPerSpawn = (minionsPerSpawn > totalMinions) ? minionsPerSpawn - totalMinions : minionsPerSpawn;

                    for (int i = 0; i < minionsPerSpawn; i++)
                    {
                        if (minionsOnScreen < maxMinionsOnScreen)
                        {
                            // 1
                            int spawnPoint = -1;
                            // 2
                            while (spawnPoint == -1)
                            {
                                // 3
                                int randomNumber = Random.Range(0, spawnPoints.Length);
                                // 4
                                if (!previousSpawnLocations.Contains(randomNumber))
                                {
                                    previousSpawnLocations.Add(randomNumber);
                                    spawnPoint = randomNumber;
                                }
                            }
                            GameObject spawnLocation = spawnPoints[spawnPoint];
                            //GameObject newMinion = Instantiate(minion);
                            //newMinion.transform.position = spawnLocation.transform.position;
                            projectilePoller.SpawnFromPool("Minions", spawnLocation.transform.position);
                            minionsOnScreen += 1;
                        }
                    }
                }
            }
        }
    }

    void StartGame()
    {
        isGameOver = false;
    }

    public void RestartGame()
    {
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.FINALBOSSGAME);
    }

    public void GoToMainMenu()
    {
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.MAINMENU);
    }

    public void RemoveEnemy()
    {
        minionsOnScreen -= 1;
    }
}
