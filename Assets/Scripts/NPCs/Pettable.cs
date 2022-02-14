// Followed this tutorial to change sprites when interacting with an object https://www.youtube.com/watch?v=GaVADPZlO0o

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pettable : Interactable
{
    public Animator animator;
    private bool isIdle;

    public override void Interact()
    {
        animator.SetBool("isPet", true);
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
