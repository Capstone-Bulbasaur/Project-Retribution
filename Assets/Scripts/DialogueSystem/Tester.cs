// Dialogue System based on the following tutorial: https://youtu.be/mhEiJ_-jyTs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public Conversation convo;

    public void StartConvo()
    {
        DialogueManager.instance.StartConversation(convo);
    }
}
