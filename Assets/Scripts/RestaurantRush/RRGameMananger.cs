using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RRGameMananger : MonoBehaviour
{
    private int rand;

    public List<GameObject> ingredients = new List<GameObject>();
    public List<Sprite> ingredientSprites = new List<Sprite>();
    public int[] order;
    public GameObject ingredientBubble;
    public GameObject rightAnswer;
    public GameObject wrongAnswer;

    // Primary game time set up
    float CurrentTime;
    public float StartingTime = 100;
    // order time set up
    public float OrderStartTime = 10;
    float OrderCurrentTime;

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
    }
    void Start()
    {
        // Shows the first order after 2 seconds and hides it after 3 seconds
        Invoke("ShowBubble", 7.0f);
       // FindObjectOfType<AudioManager>().Play("Rush_Music");

        CurrentTime = StartingTime;
    }

    void Update()
    {
        CurrentTime -= 1 * Time.deltaTime;

        if (CurrentTime != 0)
        {
            print(CurrentTime);
        }
        if (CurrentTime <= 0)
        {
            CurrentTime = 0;
        }

        Timer.text = CurrentTime.ToString("0");

        // All ingredients correct (count == 4)
        if (count == 4)
        {
            // Correct answer visuals
            rightAnswer.SetActive(true);
            Invoke("HideRightAnswer", 1.0f);

            // Shuffles a new order
            NewOrder();

            //TODO ADD CORRECT ANSWER SOUND HERE
        }

        if (incorrectKey == true)
        {
            // Incorrect answer visuals
            wrongAnswer.SetActive(true);
            Invoke("HideWrongAnswer", 1.0f);

            // Shuffles a new order
            NewOrder();

            //TODO ADD INCORRECT ANSWER SOUND HERE
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
