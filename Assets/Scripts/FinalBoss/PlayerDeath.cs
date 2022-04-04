using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    public Animator animatorRed;
    public Animator animatorDied;
    public static PlayerDeath instance;
    public GameObject playerDiedPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        playerDiedPanel.SetActive(false);
    }

    private void Start()
    {
        this.gameObject.SetActive(true);
    }

    public void FadeToRestart()
    {
        animatorRed.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        playerDiedPanel.gameObject.SetActive(true);
        AudioManager.instance.StopPlaying("Boss_Music");
        
        animatorDied.SetTrigger("FadeTextIn");
        AudioManager.instance.Play("Boss_PlayerDeath");
    }


}
