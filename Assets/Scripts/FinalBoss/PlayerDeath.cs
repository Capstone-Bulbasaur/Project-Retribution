using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    public Animator animator;
    public static PlayerDeath instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //gameObject.SetActive(true);
    }

    private void Start()
    {
        this.gameObject.SetActive(true);
    }

    public void FadeToRestart()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene((int)Constants.gameScenes.FINALBOSSGAME);
    }
}
