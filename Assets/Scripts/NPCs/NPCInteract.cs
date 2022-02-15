using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : Interactable
{
    public Conversation currentNPCConvo;
    public bool dialoguePlayed = false;
    public void FinishedConvo()
    {
        dialoguePlayed = false;
    }

    public override void Interact()
    {
        if (!dialoguePlayed)
        {
            DialogueManager.instance.StartConversation(currentNPCConvo, FinishedConvo);
            dialoguePlayed = true;
        }
    }
}
