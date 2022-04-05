using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Got part of the tutorial from: https://www.youtube.com/watch?v=CE9VOZivb3I

public class MMYouWin : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        // StartCoroutine was needed to encapsulate the WaitForSeconds and the Load scene.
        // For some reason all of that separated did not work.
        // Moved the Coroutine to Start as recommended by James
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        if(SceneManager.GetActiveScene().name == "RR-Midscene-YouWin")
            AudioManager.instance.Play("Rush_Win");
        yield return new WaitForSeconds(3.0f);
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.HUBWORLD);
    }
}
