using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private Animator animator;
     
    public void AnimationEnd(string myEvent)
    {
        if (myEvent.Equals("AnimationEnded"))
        {
            LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.MAINMENU);
        }
    }

    public void CloseCredits()
    {
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.MAINMENU);
    }
}
