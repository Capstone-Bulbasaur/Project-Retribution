// Dialogue System based on the following tutorial: https://youtu.be/mhEiJ_-jyTs
// Change UI button click on keypress https://www.youtube.com/watch?v=VFAXyUvwtYQ&t=156s

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue;
    public Image speakerSprite;
    public Button button;
    public Sprite nextDialogueSprite;
    public Sprite closeDialogueSprite;
    public GameObject Graey;
    public KeyCode actionKey;
    public Animator letsGoAnimator;
    public static DialogueManager instance;
    public delegate void ConvoFinishedCallback();
    public GameObject letsGo = null;

    private int currentIndex;
    private Conversation currentConvo;
    private Animator animator;
    private Coroutine typing;
    private ConvoFinishedCallback callback;

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

        if (letsGo != null)
            letsGo.SetActive(false);
    }

    void Update()
    {
        if (currentConvo != null)
        {
            //If user presses action key, click the button
            if(Input.GetKeyDown(actionKey))
            {
                button.onClick.Invoke();
            }
        }
    }

    void ChangePressedColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, button.colors.fadeDuration, true, true);
    }

    public void StartConversation(Conversation convo, ConvoFinishedCallback callb)
    {
        animator.SetBool("isOpen", true);
        currentIndex = 0;
        currentConvo = convo;
        speakerName.text = "";
        dialogue.text = "";
        callback = callb;

        FindObjectOfType<AudioManager>().Play("Hub_DialoguePop");
    }

    public void ReadNext()
    {
        // Closes the dialogue box if it reaches the end of the conversation
        if(currentIndex > currentConvo.GetLength())
        {
            animator.SetBool("isOpen", false);
            button.GetComponent<Image>().sprite = nextDialogueSprite;
            callback();

            if (speakerName.text == "Orry")
            {
                PlayerPrefs.SetFloat("PlayerX", Graey.transform.position.x);
                PlayerPrefs.SetFloat("PlayerY", Graey.transform.position.y);

                if (letsGo != null)
                    letsGo.SetActive(true);

                Invoke("OpenMemoryMatch", 2.5f);
                letsGoAnimator.SetBool("OpenMM", true);
            }

            if (speakerName.text == "Minion")
            {
                PlayerPrefs.SetFloat("PlayerX", Graey.transform.position.x);
                PlayerPrefs.SetFloat("PlayerY", Graey.transform.position.y);

                if (letsGo != null)
                    letsGo.SetActive(true);

                Invoke("OpenFinalBossBattle", 2.5f);
                letsGoAnimator.SetBool("OpenMM", true);
            }

            if (speakerName.text == "Embre")
            {
                PlayerPrefs.SetFloat("PlayerX", Graey.transform.position.x);
                PlayerPrefs.SetFloat("PlayerY", Graey.transform.position.y);

                if (letsGo != null)
                    letsGo.SetActive(true);

                Invoke("OpenRestaurantRush", 2.5f);
                letsGoAnimator.SetBool("OpenMM", true);
            }

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

    void OpenMemoryMatch()
    {
        FindObjectOfType<AudioManager>().StopPlaying("Hub_Music");
        SceneManager.LoadScene(sceneName: "MemoryMatch 1");
    }

    void OpenFinalBossBattle()
    {
        FindObjectOfType<AudioManager>().StopPlaying("Hub_Music");
        SceneManager.LoadScene(sceneName: "FinalBoss");
    }

    void OpenRestaurantRush()
    {
        FindObjectOfType<AudioManager>().StopPlaying("Hub_Music");
        SceneManager.LoadScene(sceneName: "RestaurantRush");
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
