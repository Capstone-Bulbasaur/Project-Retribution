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
    public Button yesButton;
    public Button noButton;
    public Button interactionButton;

    private int currentIndex;
    private Conversation currentConvo;
    private Animator animator;
    private Coroutine typing;
    private ConvoFinishedCallback callback;

    bool dialogueFinished;

    private void Awake()
    {
        DisableYesNoButtons();
        dialogueFinished = false;

        if (instance == null)
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
            // Workaround to lock interaction button in Final Battle Win dialogue. This scene doesn't have a character controller and complains because it doesn't exist
            if(Input.GetKeyDown(actionKey) && SceneManager.GetActiveScene().name == "FinalBossWinDialogue")
            {
                if(yesButton.IsActive() && noButton.IsActive())
                {
                    return;
                }
                else
                {
                    button.onClick.Invoke();
                }
            }
            //If user presses action key, click the button
            else if (Input.GetKeyDown(actionKey) && Graey.GetComponent<CharacterController>().canInteract == true)
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
        if (SceneManager.GetActiveScene().name == "HubWorld" || SceneManager.GetActiveScene().name == "GraeyHome")
        {
            DisableMovement();
        }

        button.interactable = true;
        interactionButton.interactable = true;
        animator.SetBool("isOpen", true);
        currentIndex = 0;
        currentConvo = convo;
        speakerName.text = "";
        dialogue.text = "";
        dialogueFinished = false;
        callback = callb;

        AudioManager.instance.Play("Hub_DialoguePop");
    }

    public void ReadNext()
    {
        // Closes the dialogue box if the player presses NO (No risk of accidentally going into the minigames)
        if(dialogueFinished == true)
        {
            animator.SetBool("isOpen", false);
            button.GetComponent<Image>().sprite = nextDialogueSprite;
            callback();
            if (SceneManager.GetActiveScene().name == "HubWorld")
            {
                EnableMovement();
            }

            return;
        }

        // Closes the dialogue box if it reaches the end of the conversation
        if(currentIndex > currentConvo.GetLength())
        {
            animator.SetBool("isOpen", false);
            button.GetComponent<Image>().sprite = nextDialogueSprite;
            callback();

            if (speakerName.text == "Orry")
            {
                LetsGoAnimationSceneLoad("OpenMemoryMatch");
            }
            else if (speakerName.text == "Minion")
            {
                LetsGoAnimationSceneLoad("OpenFinalBossBattle");
            } 
            else if (speakerName.text == "Embre")
            {
                LetsGoAnimationSceneLoad("OpenRestaurantRush");
            }
            else if (speakerName.text == "Gaehl")
            {
                LetsGoAnimationSceneLoad("OpenSoakinSpirit");
            }
            else if (speakerName.text == "Isarr")
            {
                LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.CELEBRATION);
            }

            if(SceneManager.GetActiveScene().name == "HubWorld" || SceneManager.GetActiveScene().name == "GraeyHome")
                EnableMovement();
            
            return;
        }

        speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();

        // Yes/no option to repeat mini games
        if (currentConvo.GetLineByIndex(currentIndex).dialogue == "Sure! Would you like to stay and help?")
        {
            button.interactable = false;
            interactionButton.interactable = false;
            Graey.GetComponent<CharacterController>().canInteract = false;
            EnableYesNoButtons();
        }

        // Yes/no options for Final Battle dialogue
        if(currentConvo.GetLineByIndex(currentIndex).dialogue == "Please let me LIVE!")
        {
            button.interactable = false;
            //interactionButton.interactable = false;
            EnableYesNoButtons();
        }

        // Checks if dialogue line is being typed and overwrites it with the next dialogue line if the player presses [next]. Avoids the end of one character's line going into the next character's dialogue
        if (typing == null)
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
    void LetsGoAnimationSceneLoad(string functionName)
    {
        PlayerPrefs.SetFloat("PlayerX", Graey.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", Graey.transform.position.y);

        if (letsGo != null)
        {
            letsGo.SetActive(true);
            Graey.GetComponent<CharacterController>().canInteract = false;
            interactionButton.interactable = false;
        }

        Invoke(functionName, 2.5f);
        letsGoAnimator.SetBool("OpenMM", true);
    }

    void DisableMovement()
    {
        Graey.GetComponent<CharacterController>().canMove = false;
        Graey.GetComponent<Animator>().enabled = false;
        Graey.GetComponentInChildren<AudioSource>().mute = true;
    }

    void EnableMovement()
    {
        Graey.GetComponent<CharacterController>().canMove = true;
        Graey.GetComponent<Animator>().enabled = true;
        Graey.GetComponentInChildren<AudioSource>().mute = false;
    }

    void OpenMemoryMatch()
    {
        AudioManager.instance.StopPlaying("Hub_Music");
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.MEMMATCHGAME);
    }

    void OpenFinalBossBattle()
    {
        AudioManager.instance.StopPlaying("Hub_Music");
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.FINALBOSSGAME);
    }

    void OpenRestaurantRush()
    {
        AudioManager.instance.StopPlaying("Hub_Music");
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.RRGAME);
    }

    void OpenSoakinSpirit()
    {
        AudioManager.instance.StopPlaying("Hub_Music");
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.FFSOAKINSPIRIT);
    }

    public void YesButton()
    {
        if(SceneManager.GetActiveScene().name == "FinalBossWinDialogue")
        {
            PlayerPrefs.SetInt("SavedIsarr", 1);
            DisableYesNoButtons();
            return;
        }
        DisableYesNoButtons();
        Graey.GetComponent<CharacterController>().canInteract = true;
    }

    public void NoButton()
    {
        if (SceneManager.GetActiveScene().name == "FinalBossWinDialogue")
        {
            PlayerPrefs.SetInt("SavedIsarr", 0);
            DisableYesNoButtons();
            return;
        }
        dialogueFinished = true;
        Graey.GetComponent<CharacterController>().canInteract = true;
        DisableYesNoButtons();
    }

    void EnableYesNoButtons()
    {
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }

    void DisableYesNoButtons()
    {
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        button.interactable = true;
        interactionButton.interactable = true;
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
