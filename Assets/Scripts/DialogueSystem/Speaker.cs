// Dialogue System based on the following tutorial: https://youtu.be/mhEiJ_-jyTs

using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialogue/New Speaker")] // Creates a New Speaker menu item in Unity's Create menu under the Dialogue tab
public class Speaker : ScriptableObject
{
    [SerializeField]private string speakerName;
    [SerializeField] private Sprite speakerSprite;

    public string GetName()
    {
        return speakerName;
    }

    public Sprite GetSprite()
    {
        return speakerSprite;
    }
}
