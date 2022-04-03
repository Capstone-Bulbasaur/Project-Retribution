using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

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
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;


    public static FFGameManager instance;
    
    private float generatedSpawnTime = 0;
    private float currentSpawnTime = 0;
    [SerializeField] private Transform gameZone;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime;

        if (currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;
            generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            StartCoroutine(SpawnFires(Random.Range(minSpawnTime, maxSpawnTime)));//BoilerPlate, might not be the final logic we use but can be used as a template maybe?
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void SpawnFire() //Boilerplate spawning logic we've been using, nothing particularly wrong with it (minor known bug of maxperspawn not working)
    {
        ////SPAWN FLAMES CODE
        //currentSpawnTime += Time.deltaTime;

        //if (currentSpawnTime > generatedSpawnTime)
        //{
        //    currentSpawnTime = 0;
        //    generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

        //    if (flamesPerSpawn > 0 && flamesOnScreen < totalFlames)
        //    {
        //        List<int> previousSpawnLocations = new List<int>();

        //        if (flamesPerSpawn > spawnPoints.Length)
        //        {
        //            flamesPerSpawn = spawnPoints.Length - 1;
        //        }

        //        flamesPerSpawn = (flamesPerSpawn > totalFlames) ? flamesPerSpawn - totalFlames : flamesPerSpawn;

        //        for (int i = 0; i < flamesPerSpawn; i++)
        //        {
        //            if (flamesOnScreen < maxFlamesOnScreen)
        //            {
        //                int spawnPoint = -1;

        //                while (spawnPoint == -1)
        //                {
        //                    int randomNumber = Random.Range(0, spawnPoints.Length);

        //                    if (!previousSpawnLocations.Contains(randomNumber))
        //                    {
        //                        previousSpawnLocations.Add(randomNumber);
        //                        spawnPoint = randomNumber;
        //                    }

        //                    GameObject spawnLocation = spawnPoints[spawnPoint];
        //                    GameObject newFlame = Instantiate(flame);
        //                    newFlame.transform.SetParent(spawnLocation.gameObject.transform, false);
        //                    newFlame.transform.position = spawnLocation.transform.position;
        //                    flamesOnScreen += 1;
        //                }
        //            }
        //        }
        //    }
        //}

        GameObject spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)];
        bool spawned = false;

        //Check if there's already 18 fires on screen
        if (flamesOnScreen < spawnPoints.Length)
        {
            while (!spawned)
            {
                if (spawnLocation.transform.childCount == 0)
                {
                    GameObject fires = Instantiate(flame, spawnLocation.gameObject.transform, false);
                    //Sound Effect?
                    //Fwoosh?
                    fires.transform.position = spawnLocation.transform.position;
                    flamesOnScreen += 1;
                    spawned = true;
                }
                else
                {
                    spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)];
                }
            }
        }
    }

    public void RemoveFlame()
    {
        flamesOnScreen -= 1;
    }

    IEnumerator SpawnFires(float spawn)
    {
        yield return new WaitForSeconds(spawn);

        SpawnFire();
    }



    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;


    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;


    }

    public void RetrunMainMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
        Time.timeScale = 1f;
    }
}
