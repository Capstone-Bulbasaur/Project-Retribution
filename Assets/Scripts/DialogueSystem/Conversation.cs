// Dialogue System based on the following tutorial: https://youtu.be/mhEiJ_-jyTs

using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/NewConversation")]
public class Conversation : ScriptableObject
{
    [SerializeField] private DialogueLine[] allLines;

    public DialogueLine GetLineByIndex(int index)
    {
        return allLines[index];
    }

    public int GetLength()
    {
        return allLines.Length - 1;
    }
}
