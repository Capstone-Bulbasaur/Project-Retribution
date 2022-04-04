using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.HUBWORLD);
        }
    }
}
