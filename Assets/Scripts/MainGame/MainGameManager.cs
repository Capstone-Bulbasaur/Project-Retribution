using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainGameManager : MonoBehaviour
{
    private static MainGameManager instance;

    public string playerName;
    public int npcHelpCounter;
    public bool recruitedOrry;
    public bool recruitedGaehl;
    public bool recruitedEmbre;
    public Vector2 GraeyPosition;
    
    public GameObject Graey;

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
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
        {
            Graey.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
        }
    }

    public void StartScene()
    {
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene("HubWorld");
    }

    public void ContinueScene()
    {
       SceneManager.LoadScene("HubWorld");
    }

    
}
