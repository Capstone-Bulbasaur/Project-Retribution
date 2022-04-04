using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public int brokenWindows = 0;

    public GameObject youLosePanel;
    public bool gameOver = false;

    public static FFGameManager instance;

    private float generatedSpawnTime = 0;
    private float currentSpawnTime = 0;
    private float WaitforInstructions = 6.0f; // TODO - fix this hardcoded after Level Up.
    [SerializeField] private Transform gameZone;
    private bool isHalfTime = false;

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

        //youLosePanel.gameObject.SetActive(false); It was throwing an error that the YouLose panel was not assigned. After commenting out this line, and disabling the panel manually (unity), works fine
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {

            if (FFUIManager.instance.currentTime <= FFUIManager.instance.startingTime / 2 && isHalfTime == false) //the fires should spawn faster halfway through the game
            {
                maxSpawnTime -= maxSpawnTime / 2.0f;
                isHalfTime = true;
            }
            WaitforInstructions -= Time.deltaTime;
            if (WaitforInstructions < 0)
            {
                currentSpawnTime += Time.deltaTime;
                if (currentSpawnTime > generatedSpawnTime)
                {
                    currentSpawnTime = 0;
                    generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                    StartCoroutine(SpawnFires(Random.Range(minSpawnTime,
                        maxSpawnTime)));
                }
                if (brokenWindows == 5)
                {
                    //restart game
                    StartCoroutine(TryAgain());
                    return;
                }
                
                else if (FFUIManager.instance.currentTime <= 0.5f)
                {
                    //Recruited Gaehl
                    PlayerPrefs.SetInt("RecruitedGaehl", 1);

                    // Loads the FF You Win scene with the message, and then, loads the Hub World
                    LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.FFSOAKINSPIRITYOUWIN);
                }
            }
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

    public void SpawnFire()
    {
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


    IEnumerator TryAgain()
    {
        // got part of the Lose panel solution on this tutorial: https://www.youtube.com/watch?v=e0feEWLRSYI
        youLosePanel.gameObject.SetActive(true); // make the youLosePanel visible if is the 5th fail
        yield return new WaitForSeconds(2.0f); // wait for 2s
        youLosePanel.gameObject.SetActive(false); // make the youLosePanel invisible again before the Restart Game
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.FFSOAKINSPIRIT);
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
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.MAINMENU);
        Time.timeScale = 1f;
    }
}
