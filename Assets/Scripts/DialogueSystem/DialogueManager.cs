// Dialogue System based on the following tutorial: https://youtu.be/mhEiJ_-jyTs

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue;
    public Image speakerSprite;
    public Button button;
    public Sprite nextDialogueSprite;
    public Sprite closeDialogueSprite;

    private int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;
    private Animator animator;
    private Coroutine typing;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            animator = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void StartConversation(Conversation convo)
    {
        instance.animator.SetBool("isOpen", true);
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";

        instance.ReadNext();
    }

    public void ReadNext()
    {
        // Closes the dialogue box if it reaches the end of the conversation
        if(currentIndex > currentConvo.GetLength())
        {
            instance.animator.SetBool("isOpen", false);
            button.GetComponent<Image>().sprite = nextDialogueSprite;
            return;
        }

        speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();

        // Checks if dialogue line is being typed and overwrites it with the next dialogue line if the player presses [next]. Avoids the end of one character's line going into the next character's dialogue
        if(typing == null)
        {
            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
        }
        else
        {
            instance.StopCoroutine(typing);
            typing = null;
            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
        }

        speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();
        currentIndex++;

        // Changes the button sprite so the player knows it's the last line in the conversation
        if(currentIndex >= currentConvo.GetLength() + 1)
        {
            button.GetComponent<Image>().sprite = closeDialogueSprite;
        }
    }

    // Types out the dialogue lines
    private IEnumerator TypeText(string text)
    {
        dialogue.text = "";
        bool complete = false;
        int index = 0;

        while (!complete)
        {
            dialogue.text += text[index];
            index++;
            yield return new WaitForSeconds(0.03f);

            if(index == text.Length)
            {
                complete = true;
            }
        }
        
        typing = null;
    }
}
