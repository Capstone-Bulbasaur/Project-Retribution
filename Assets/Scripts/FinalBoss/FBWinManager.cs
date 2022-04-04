using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBWinManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.instance.Play("Boss_IsarrDeath");

        var j = FindObjectOfType<NPCInteract>();
        j.Interact();

        var f = FindObjectOfType<DialogueManager>();
        f.ReadNext();
    }
}
