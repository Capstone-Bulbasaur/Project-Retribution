using System.Collections;
using UnityEngine;

// Got part of the tutorial from: https://www.youtube.com/watch?v=CE9VOZivb3I

public class MMYouWin : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // StartCoroutine was needed to encapsulate the WaitForSeconds and the Load scene.
        // For some reason all of that separated did not work.
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2.0f);
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.HUBWORLD);
    }
}
