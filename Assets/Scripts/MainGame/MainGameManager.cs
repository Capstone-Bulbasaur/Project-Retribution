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


    public Vector2 GetGraeyPosition() // not using it
    {
        return GraeyPosition;
    }

    public void SetGraeyPosition() // not using it
    {
        GraeyPosition = Graey.transform.position;
    }
    
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
        if (SceneManager.GetActiveScene().name == "HubWorld")
        {
            AudioManager.instance.StopPlaying("Menu_Music");
            AudioManager.instance.Play("Hub_Music");
        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            AudioManager.instance.Play("Menu_Music");
        }
        else
        {
            if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
            {
                Graey.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
            }

            if (PlayerPrefs.HasKey("RecruitedOrry") || PlayerPrefs.HasKey("RecruitedGaehl") ||
                PlayerPrefs.HasKey("RecruitedEmbre"))
            {
                recruitedOrry = (PlayerPrefs.GetInt("RecruitedOrry") != 0);
                recruitedGaehl = (PlayerPrefs.GetInt("RecruitedGaehl") != 0);
                recruitedEmbre = (PlayerPrefs.GetInt("RecruitedEmbre") != 0);
            }

            Debug.Log("Is orry recruited: " + recruitedOrry);
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
        AudioManager.instance.StopPlaying("Menu_Music");
        SceneManager.LoadScene("HubWorld");
    }

    public void ContinueScene()
    { 
       AudioManager.instance.StopPlaying("Menu_Music");
       SceneManager.LoadScene("HubWorld");
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



    void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
