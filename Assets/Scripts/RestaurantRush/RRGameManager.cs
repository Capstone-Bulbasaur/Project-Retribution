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

    public List<GameObject> ingredients = new List<GameObject>();
    public List<Sprite> ingredientSprites = new List<Sprite>();
    public int[] order;
    public GameObject ingredientBubble;
    public GameObject rightAnswer;
    public GameObject wrongAnswer;
    public List<Button> btns = new List<Button>();

    // MS - getting the example from MM, to apply the textMeshPro for player score and Missed guesses
    public TextMeshProUGUI RightGuesses;
    //public TextMeshProUGUI WrongGuesses;
    public TextMeshProUGUI YouWon;

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

    bool incorrectKey = false;
    int count = 0;
    Dictionary<int, string> Buttons = new Dictionary<int, string>();

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
        //Disable buttons
        for (int i = 0; i < 4; i++)
        {
            btns[i].interactable = false;
        }

        // Shows the first order after 2 seconds and hides it after 3 seconds
        Invoke("ShowBubble", 7.0f);
        FindObjectOfType<AudioManager>().Play("Rush_Music");

        CurrentTime = StartingTime;

        //AddBurgerstoList();

        foreach (GameObject Burger in burgers) // using directly in update
        {
            Burger.SetActive(false);
        }
    }

    //Add burgers to the list
    void AddBurgerstoList()
    {
        GameObject[] brgr = GameObject.FindGameObjectsWithTag("Burger");
        
        // MS - still not sure if this loop is right
        //for (int i = 0; i < brgr.Length; i++)
        //{
        //    burgers.Add(brgr[i].GetComponentInParent<GameObject>());
        //}
    }

    // making the burgers on the list appear for every right match
    void OnTriggerEnter()
    {
        GameObject[] brgr = GameObject.FindGameObjectsWithTag("Burger");
        foreach(GameObject Burger in burgers) // using directly in update
        {
            Burger.SetActive(true);
        }
                
    }

    void Update()
    {
        CurrentTime -= 1 * Time.deltaTime;
        WhiteTimer -= 1 * Time.deltaTime;
        OrderCurrentTime -= 1 * Time.deltaTime;


        // MS - commented out to see other events on the console .
        //if (CurrentTime != 0)
        //{
        //    print(CurrentTime);
        //}
        //if (CurrentTime <= 0)
        //{
        //    CurrentTime = 0;
        //}
        if (WhiteTimer <= 0)
        {
            Timer.color = Color.white;


        }
        if (CurrentTime >= 0)
        {
            SceneManager.LoadScene(sceneName: "HubWorld");
        }

        if (countCorrectGuesses == 6)
        {
            SceneManager.LoadScene(sceneName: "HubWorld");
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
                Debug.Log("YOU WON =D"); //YouWon.ToString();
            }
            
            // MS - increasing the correct guesses
            countCorrectGuesses++;
            RightGuesses.text = countCorrectGuesses.ToString();
                  
            
            Debug.Log("right answer, new order done!");

            //Disable buttons
            for (int i = 0; i < 4; i++)
            {
                btns[i].interactable = false;
            }

            Invoke("HideRightAnswer", 1.0f);
                    

            // Shuffles a new order
            NewOrder();

            //TODO ADD CORRECT ANSWER SOUND HERE
            FindObjectOfType<AudioManager>().Play("Rush_Correct");
        }
         

        if (incorrectKey == true)
        {
            CurrentTime = CurrentTime - 5;
            OrderCurrentTime = OrderStartTime;
            // Incorrect answer visuals
            wrongAnswer.SetActive(true);
            // MS
            countFails++;
            //WrongGuesses.text = countFails.ToString();

            //EmptyPlate.SetActive(true);
            Debug.Log("wrong answer, new empty place!");

            //Disable buttons
            for (int i = 0; i < 4; i++)
            {
                btns[i].interactable = false;
            }

            Invoke("HideWrongAnswer", 1.0f);

            //Invoke("HidePlate", 1.0f);

            // Shuffles a new order
            NewOrder();

            //TODO ADD INCORRECT ANSWER SOUND HERE
            FindObjectOfType<AudioManager>().Play("Rush_Incorrect");

            Timer.color = Color.red;
            WhiteTimer = 3;
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


}
