using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBWinManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var j = FindObjectOfType<NPCInteract>();
        j.Interact();

        var f = FindObjectOfType<DialogueManager>();
        f.ReadNext();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}