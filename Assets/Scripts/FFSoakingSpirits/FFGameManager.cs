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
    public int brokenWindows = 0;
    public GameObject youLosePanel;

    public static FFGameManager instance;

    public static RRGameManager RRinstance;

    private bool gameOver = false;
    private float generatedSpawnTime = 0;
    private float currentSpawnTime = 0;
    private float WaitforInstructions = 6.0f; // TODO - fix this hardcoded after Level Up.
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
        if (!gameOver)
        {
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
                    gameOver = true;
                    //restart game
                    //try again screen
                    StartCoroutine(TryAgain());
                    gameOver = true;
                    return;
                }
                else if (FFUIManager.instance.currentTime == 0)
                {
                    //you win? I guess
                    //recruit ghaeli
                    //load back to hubworld
                }
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

    void Restart()
    {
        // need to throw the logic here
        RemoveFlame();
        gameOver = false;
    }

    IEnumerator TryAgain()
    {
        // got part of the Lose panel solution on this tutorial: https://www.youtube.com/watch?v=e0feEWLRSYI
        youLosePanel.gameObject.SetActive(true); // make the youLosePanel visible if is the 5th fail
        yield return new WaitForSeconds(2.0f); // wait for 2s
        youLosePanel.gameObject.SetActive(false); // make the youLosePanel invisible again before the Restart Game
        Restart();
    }
}
