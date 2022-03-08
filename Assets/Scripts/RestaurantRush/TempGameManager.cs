using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TempGameManager : MonoBehaviour
{
    private int rand;

    public List<GameObject> ingredients = new List<GameObject>();
    public List<Sprite> ingredientSprites = new List<Sprite>();
    public List<Button> btns = new List<Button>();
    public int[] order;
    public GameObject ingredientBubble;
    public GameObject rightAnswer;
    public GameObject wrongAnswer;

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
        Invoke("ShowBubble", 2.0f);
        Invoke("HideBubble", 3.0f);
    }

    void Update()
    {
        if(count == 4)
        {
            // Correct answer visuals
            rightAnswer.SetActive(true);
            Invoke("HideRightAnswer", 1.0f);

            NewOrder();

            //TODO ADD CORRECT ANSWER SOUND HERE
        }

        if (incorrectKey == true)
        {
            wrongAnswer.SetActive(true);
            Invoke("HideWrongAnswer", 1.0f);

            NewOrder();

            //TODO ADD INCORRECT ANSWER SOUND HERE
        }

        if (count <= 3)
        {
            //CheckInput();
        }
    }

    void IngredientOrder()
    {
        order = new int[ingredients.Count];
        for (int i = 0; i < ingredients.Count; i++)
        {
            order[i] = Random.Range(0, ingredientSprites.Count);
        }

        for (int i = 0; i < ingredients.Count; i++)
        {
            ingredients[i].GetComponent<SpriteRenderer>().sprite = ingredientSprites[order[i]];
        }
    }

    public void buttonClicked(string button)
    {

        if (Buttons[order[count]] == button)
        {
            Debug.Log("Correct button " + button);
            count++;
        }
        else
        {
            Debug.Log("Incorrect button " + button);
            incorrectKey = true;
        }
        
    }

    void NewOrder()
    {
        Invoke("ShowBubble", 2.0f);
        count = 0;
        Invoke("HideBubble", 3.0f);
        incorrectKey = false;
    }

    void ShowBubble()
    {
        IngredientOrder();
        ingredientBubble.SetActive(true);
    }

    void HideBubble()
    {
        ingredientBubble.SetActive(false);
    }

    void HideRightAnswer()
    {
        rightAnswer.SetActive(false);
    }

    void HideWrongAnswer()
    {
        wrongAnswer.SetActive(false);
    }
}
