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
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

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
        }
    }

    public void StartScene()
    {
        PlayerPrefs.DeleteAll();
        ContinueScene();
    }

    public void ContinueScene()
    { 
       AudioManager.instance.StopPlaying("Menu_Music");
       LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.HUBWORLD);
    }

    public void LoadCredits()
    {
        AudioManager.instance.StopPlaying("Menu_Music");
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.CREDITS);
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
        SceneManager.LoadScene(sceneName: "MainMenu");
        Time.timeScale = 1f;
    }
}
