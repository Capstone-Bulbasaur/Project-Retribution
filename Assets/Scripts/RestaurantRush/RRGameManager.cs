using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class RRGameManager : MonoBehaviour
{
    private int rand;
    // MS - to keep track of the gameGuesses
    private int gameGuesses;
    private int countGuesses;
    private int countCorrectGuesses;
    private int countFails;
    private int RightMatch;
    private bool waitforInstructions = false;
    public Animator instructionAnimator;

    public List<GameObject> ingredients = new List<GameObject>();
    public List<Sprite> ingredientSprites = new List<Sprite>();
    public int[] order;
    public GameObject ingredientBubble;
    public GameObject rightAnswer;
    public GameObject wrongAnswer;
    public List<Button> btns = new List<Button>();
    public GameObject youLosePanel;
    public int MaxFails;

    // MS - getting the example from MM, to apply the textMeshPro for player score and Missed guesses
    public TextMeshProUGUI RightGuesses;
    //public TextMeshProUGUI WrongGuesses;
    public TextMeshProUGUI YouWon;

    public TextMeshProUGUI RedTimer; // MS

    public List<GameObject> burgers = new List<GameObject>();

    public GameObject EmptyPlate;

    // Primary game time set up
    float CurrentTime;
    public float StartingTime = 100;
    // order time set up
    public float OrderStartTime = 10;
    float OrderCurrentTime;

    public float WhiteTimer = 3;

    public int maxNumCustomers;
    int CurrentNumCustomers;

    public TextMeshProUGUI Timer;

    bool isGameOver = false;
    bool incorrectKey = false;
    int count = 0;
    Dictionary<int, string> Buttons = new Dictionary<int, string>();
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    void Awake()
    {
        Buttons.Add(0, "Y");
        Buttons.Add(1, "X");
        Buttons.Add(2, "A");
        Buttons.Add(3, "B");

        ingredientBubble.SetActive(false);
        rightAnswer.SetActive(false);
        wrongAnswer.SetActive(false);
        CurrentTime = StartingTime;
        OrderCurrentTime = OrderStartTime;

    }
    void Start()
    {
        //CurrentTime = StartingTime;
        //Disable buttons
        for (int i = 0; i < 4; i++)
        {
            btns[i].interactable = false;
        }

        //if (waitforInstructions == true)
        //{
        //    Invoke("ShowBubble", 1.0f);
        //}
        AudioManager.instance.Play("Rush_Music");

        
        foreach (GameObject Burger in burgers) // using directly in update
        {
            Burger.SetActive(false);
        }

        // Starting the game setting the youLose panel to inactive.
        youLosePanel.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (!isGameOver)
        {
            // Solution for the issue we had before when the 1st button was always wrong
            
            if (waitforInstructions == true)
            {
                //Show the first bubble
                if (CurrentTime == 99)
                {
                    Invoke("ShowBubble", 2.0f);
                }

                if (CurrentTime <= 0)
                {
                    StartCoroutine(TryAgain());
                    isGameOver = true;
                    return;
                    
                }
                else
                {
                    CurrentTime -= 1 * Time.deltaTime;
                    WhiteTimer -= 1 * Time.deltaTime;
                    OrderCurrentTime -= 1 * Time.deltaTime;
                }
            }

            if (WhiteTimer <= 0)
            {
                Timer.color = Color.white;
            }

            if (countCorrectGuesses == 6)
            {
                //Recruited Embre            
                PlayerPrefs.SetInt("RecruitedEmbre", 1);

                // Loads the RR You Win scene with the message, and then, loads the Hub World
                LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.RRYOUWIN);
            }

            Timer.text = CurrentTime.ToString("0");

            if (OrderCurrentTime <= 0)
            {
                incorrectKey = true;
            }

            // All ingredients correct (count == 4)
            if (count == 4)
            {
                OrderCurrentTime = OrderStartTime;
                // Correct answer visuals
                rightAnswer.SetActive(true);

                if (countCorrectGuesses < burgers.Count)
                {
                    burgers[countCorrectGuesses].SetActive(true);
                }
                else
                {
                    Debug.Log("YOU WON =D"); 
                                        
                }

                // MS - increasing the correct guesses
                countCorrectGuesses++;
                RightGuesses.text = countCorrectGuesses.ToString();
                       
                //Disable buttons
                for (int i = 0; i < 4; i++)
                {
                    btns[i].interactable = false;
                }

                Invoke("HideRightAnswer", 1.0f);

                // Shuffles a new order
                NewOrder();

                // CORRECT ANSWER SOUND 
                AudioManager.instance.Play("Rush_Correct");
            }


            if (incorrectKey == true)
            {
                CurrentTime = CurrentTime - 5;
                OrderCurrentTime = OrderStartTime;
                // Incorrect answer visuals
                wrongAnswer.SetActive(true);
                // Increasing the fails counts
                countFails++;
                
                Debug.Log("wrong answer, new empty place!");

                //Disable buttons
                for (int i = 0; i < 4; i++)
                {
                    btns[i].interactable = false;
                }

                Invoke("HideWrongAnswer", 1.0f);

                // Shuffles a new order
                NewOrder();

                //TODO ADD INCORRECT ANSWER SOUND HERE
                AudioManager.instance.Play("Rush_Incorrect");
                
                // added an outline on timer. Reduced WhitTimer to 1, because 3s was too much.
                Timer.color = new Color(0.9f,0.2f,0.2f,1);
                WhiteTimer = 1;
                RedTimer.outlineWidth = 0.15f;
                RedTimer.outlineColor = new Color32(255, 255, 255, 255); // white outline
                
                // Another lose condition, when the player has X wrong matches, the game will restart.
                if (countFails >= MaxFails)
                {
                    StartCoroutine(TryAgain());
                    isGameOver = true;
                    return;
                }
            }
        }
        
    }

    void IngredientOrder()
    {
        order = new int[ingredients.Count];

        // Shuffle ingredients in list
        for (int i = 0; i < ingredients.Count; i++)
        {
            order[i] = Random.Range(0, ingredientSprites.Count);
        }

        // Assign correct sprite to shuffled ingredient
        for (int i = 0; i < ingredients.Count; i++)
        {
            ingredients[i].GetComponent<SpriteRenderer>().sprite = ingredientSprites[order[i]];
        }
    }

    public void buttonClicked(string button)
    {
        // If the button required matches the button pressed, button is correct and we add 1 to the (correct button) count
        if (Buttons[order[count]] == button)
        {
            Debug.Log("Correct button " + button);
            count++;
        }
        else
        {
            // It the button pressed doesn't match, the button is incorrect and we get a new order (Update())
            Debug.Log("Incorrect button " + button);
            incorrectKey = true;

        }
    }

    // Shows order with new bubble, resets the count 
    void NewOrder()
    {
        Invoke("ShowBubble", 2.0f);
        count = 0;

        Invoke("HideBubble", 3.0f);
        incorrectKey = false;
    }

    // Shuffles the order and shows the ingredient bubble
    void ShowBubble()
    {
        IngredientOrder();

        ingredientBubble.SetActive(true);

        //Enable buttons
        for (int i = 0; i < 4; i++)
        {
            btns[i].interactable = true;
        }

        Invoke("HideBubble", 2.0f);
    }

    // Hides the ingredient bubble
    void HideBubble()
    {
        ingredientBubble.SetActive(false);
    }

    // Hides the "right answer" visual
    void HideRightAnswer()
    {
        rightAnswer.SetActive(false);
    }

    // Hides the "wrong answer" visual
    void HideWrongAnswer()
    {
        wrongAnswer.SetActive(false);
    }

    void Restart()
    {
        Timer.color = Color.white;
        Debug.Log("Restarting game...");
        // hiding the burgers again
        foreach (GameObject Burger in burgers)
        {
            Burger.SetActive(false);
        }
        // setting all variables to their original values
        countCorrectGuesses = countFails = 0;
        CurrentTime = StartingTime;
        OrderCurrentTime = OrderStartTime;
        WhiteTimer = 3;

        isGameOver = false;
        NewOrder();
        //// Calling the new Order/Ingredients again
        //Invoke("ShowBubble", 2.0f);
    }

    IEnumerator TryAgain()
    { 
        // got part of the Lose panel solution on this tutorial: https://www.youtube.com/watch?v=e0feEWLRSYI
        youLosePanel.gameObject.SetActive(true); // make the youLosePanel visible if is the 5th fail
        yield return new WaitForSeconds(2.0f); // wait for 2s
        youLosePanel.gameObject.SetActive(false); // make the youLosePanel invisible again before the Restart Game
        Restart();
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ReturnMainMenu()
    {
        //RetrunMainMenu it was before, changed the name
        LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.MAINMENU);
        Time.timeScale = 1f;
    }

    public void CloseInstructions()
    {
        instructionAnimator.SetBool("closeInstructions", true);
        waitforInstructions = true;
    }
}
