using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainGameManager : MonoBehaviour
{
    private static MainGameManager instance;

    public string playerName;
    public int npcHelpCounter;
    public bool recruitedOrry;
    public bool recruitedGaehl;
    public bool recruitedEmbre;

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

    }


    // Update is called once per frame
    void Update()
    {

        void SaveGame()
        {


        }

        void GetSaveState()
        {


        }

    }

    //private Save CreateSaveGameObject() [MF] still in progress
    //{
    //    Save save = new Save();
    //    PlayerPrefs Graey = GameObject.GetComponent<Player>();
    //    save.npcHelpCounter.Add();
    //    save.isEmbreRecruited = isEmbreRecruited;

    //    return save;
    //}

}
