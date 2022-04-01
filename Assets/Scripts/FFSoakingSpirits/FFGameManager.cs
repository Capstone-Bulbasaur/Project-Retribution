using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FFGameManager : MonoBehaviour
{ 
    //I imagine I could make a fire instance in the manager and summon all the functions here to replace this boilerplate spawning method.
    //Definitely ready for that team collaboration now.
    public GameObject flame;
    public GameObject[] spawnPoints;
    public int maxFlamesOnScreen;
    public int totalFlames;
    public int flamesPerSpawn;
    public float minSpawnTime;
    public float maxSpawnTime;
    public int flamesOnScreen = 0;
    
    private float generatedSpawnTime = 0;
    private float currentSpawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnFire(); //BoilerPlate, might not be the final logic we use but can be used as a template maybe?
    }

    public void SpawnFire() //Boilerplate spawning logic we've been using, nothing particularly wrong with it (minor known bug of maxperspawn not working)
    {
        //SPAWN FLAMES CODE
        currentSpawnTime += Time.deltaTime;

        if (currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;
            generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            if (flamesPerSpawn > 0 && flamesOnScreen < totalFlames)
            {
                List<int> previousSpawnLocations = new List<int>();

                if (flamesPerSpawn > spawnPoints.Length)
                {
                    flamesPerSpawn = spawnPoints.Length - 1;
                }

                flamesPerSpawn = (flamesPerSpawn > totalFlames) ? flamesPerSpawn - totalFlames : flamesPerSpawn;

                for (int i = 0; i < flamesPerSpawn; i++)
                {
                    if (flamesOnScreen < maxFlamesOnScreen)
                    {
                        int spawnPoint = -1;

                        while (spawnPoint == -1)
                        {
                            int randomNumber = Random.Range(0, spawnPoints.Length);

                            if (!previousSpawnLocations.Contains(randomNumber))
                            {
                                previousSpawnLocations.Add(randomNumber);
                                spawnPoint = randomNumber;
                            }

                            GameObject spawnLocation = spawnPoints[spawnPoint];
                            GameObject newFlame = Instantiate(flame);
                            newFlame.transform.position = spawnLocation.transform.position;
                            flamesOnScreen += 1;
                        }
                    }
                }
            }
        }

    }

}
