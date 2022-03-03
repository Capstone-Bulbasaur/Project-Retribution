using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBGameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject isarr;
    public GameObject minion;
    public GameObject[] spawnPoints;
    public int maxMinionsOnScreen;
    public int totalMinions;
    public int minionsPerSpawn;
    public float minSpawnTime; //UML has this as a float, anyone care?
    public float maxSpawnTime; //UML also says float

    private int minionsOnScreen = 0;
    private float generatedSpawnTime = 0;
    private float currentSpawnTime = 0;



    private void RemoveEnemy()
    {
        minionsOnScreen -= 1;
        totalMinions -= 1;

    }

    private void PauseMenu()
    {
        //functionality to enter pause menu will go here
    }

    private void GameOver()
    {
        //restart scene, flash a canvas of "You Died" or sumthin
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
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
                        minionsOnScreen += 1;
                        // 1
                        int spawnPoint = -1;
                        // 2
                        while (spawnPoint == -1)
                        {
                            // 3
                            int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                            // 4
                            if (!previousSpawnLocations.Contains(randomNumber))
                            {
                                previousSpawnLocations.Add(randomNumber);
                                spawnPoint = randomNumber;
                            }
                        }
                        GameObject spawnLocation = spawnPoints[spawnPoint];
                        GameObject newMinion = Instantiate(minion) as GameObject;
                        newMinion.transform.position = spawnLocation.transform.position;

                        //need minionScript equivalent of below functions: target, OnDestroy
                        //Alien alienScript = newAlien.GetComponent<Alien>();
                        //alienScript.target = player.transform;
                        //Minion minionScript = newMinion.GetComponent<Minion>(); //need a Minion class
                        //minionScript.target = player.transform; //with a .target function

                        
                        //minionScript.OnDestroy.AddListener(RemoveEnemy); //need an OnDestroy function from the Minion class
                        //alienScript.OnDestroy.AddListener(AlienDestroyed);
                        //alienScript.GetDeathParticles().SetDeathFloor(deathFloor);
                    }
                }
            }
        }
    }
}
