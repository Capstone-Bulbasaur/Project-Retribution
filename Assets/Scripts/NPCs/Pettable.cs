// Followed this tutorial to add interaction bubbles when there's an interactable NPC https://www.youtube.com/watch?v=GaVADPZlO0o. The sprite change is handled by the animator attached to the NPC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pettable : Interactable
{
    public Animator animator;
    public Conversation currentConvo;
    public bool dialoguePlayed = false;
    private bool isIdle;

    public override void Interact()
    {
        animator.SetBool("isPet", true);

        if (!dialoguePlayed)
        {
            DialogueManager.instance.StartConversation(currentConvo);
            dialoguePlayed = true;
        }
    }

    private void Start()
    {
        animator.SetBool("isPet", false);
    }

    void ResetBool()
    {
        animator.SetBool("isPet", false);
    }
}
