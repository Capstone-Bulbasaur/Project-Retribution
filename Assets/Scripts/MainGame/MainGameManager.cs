using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager instance;

    public string playerName;
    public int npcHelpCounter;
    public bool recruitedOrry;
    public bool recruitedGaehl;
    public bool recruitedEmbre;
    public Vector2 GraeyPosition;
    public GameObject Graey;

    public GameObject RoadblockFire;
    public GameObject CityFires;
    public GameObject RoadblockPots;
    public GameObject RoadBlockMinion;
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    public GameObject Gaehl;
    public Conversation[] NewGaehl;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        //Check if the scene is the hub world
        if (SceneManager.GetActiveScene().name == "HubWorld")
        {
            AudioManager.instance.StopPlaying("Menu_Music");
            AudioManager.instance.Play("Hub_Music");
        }

        //Check if the scene is the main menu
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            AudioManager.instance.Play("Menu_Music");
        }
        else
        {
            //Set players position to the saved player preference location
            if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
            {
                Graey.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
            }

            //Set the ally recruitment
            if (PlayerPrefs.HasKey("RecruitedOrry") || PlayerPrefs.HasKey("RecruitedGaehl") ||
                PlayerPrefs.HasKey("RecruitedEmbre"))
            {
                recruitedOrry = (PlayerPrefs.GetInt("RecruitedOrry") != 0);
                recruitedGaehl = (PlayerPrefs.GetInt("RecruitedGaehl") != 0);
                recruitedEmbre = (PlayerPrefs.GetInt("RecruitedEmbre") != 0);
            }

            if(recruitedGaehl == true)
            {
                RoadblockFire.SetActive(false);
                CityFires.SetActive(false);
                Gaehl.GetComponent<NPCInteract>().currentNPCConvo = NewGaehl[0];
            }

            if(recruitedOrry == true)
            {
                RoadblockPots.SetActive(false);
                Gaehl.GetComponent<NPCInteract>().currentNPCConvo = NewGaehl[1];
            }

            if(recruitedEmbre == true)
            {
                RoadBlockMinion.SetActive(false);
            }
        }
    }


    void Update()
    {
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

    public void StartScene()
    {
        PlayerPrefs.DeleteAll();
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.STARTINGSCENE);
    }

    public void ContinueScene()
    {
        AudioManager.instance.StopPlaying("Menu_Music");
        //Set players position to the saved player preference location
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
        {
            LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.HUBWORLD);
        }
        else
        {
            LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.STARTINGSCENE);
        }
    }

    public void LoadCredits()
    {
        AudioManager.instance.StopPlaying("Menu_Music");
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.CREDITS);
    }

    public void RecruitAlly(int character)
    {
        switch (character)
        {
            case Constants.Orry:
                recruitedOrry = true;
                PlayerPrefs.SetInt("RecruitedOrry", 1);
                break;
            case Constants.Gaehl:
                recruitedGaehl = true;
                PlayerPrefs.SetInt("RecruitedGaehl", 1);
                break;
            case Constants.Embre:
                recruitedEmbre = true;
                PlayerPrefs.SetInt("RecruitedEmbre", 1);
                break;
            default:
                Debug.LogError("Invalid ally type passed");
                break;
        }
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
