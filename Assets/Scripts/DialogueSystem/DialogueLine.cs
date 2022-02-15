// Dialogue System based on the following tutorial: https://youtu.be/mhEiJ_-jyTs

using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public Speaker speaker;
    [TextArea]
    public string dialogue;
}
