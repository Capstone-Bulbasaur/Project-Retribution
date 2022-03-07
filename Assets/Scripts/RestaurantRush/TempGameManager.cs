using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempGameManager : MonoBehaviour
{
    private int rand;

    public List<GameObject> ingredients = new List<GameObject>();
    public List<Sprite> ingredientSprites = new List<Sprite>();
    public int[] order;
    public GameObject ingredientBubble;
    public GameObject rightAnswer;
    public GameObject wrongAnswer;

    bool incorrectKey = false;
    int count = 0;
    Dictionary<int, UnityEngine.KeyCode> Keys = new Dictionary<int, UnityEngine.KeyCode>();

    void Awake()
    {
        Keys.Add(0, KeyCode.W);
        Keys.Add(1, KeyCode.A);
        Keys.Add(2, KeyCode.S);
        Keys.Add(3, KeyCode.D);
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

            Invoke("ShowBubble", 2.0f);
            count = 0;
            Invoke("HideBubble", 3.0f);
            //TODO ADD SOUND HERE
        }

        if (incorrectKey == true)
        {
            incorrectKey = false;
            wrongAnswer.SetActive(true);
            Invoke("HideWrongAnswer", 1.0f);

            Invoke("ShowBubble", 2.0f);
            count = 0;
            Invoke("HideBubble", 3.0f);

            // Inorrect answer visuals
            //TODO ADD SOUND HERE
        }

        if (count <= 3)
        {
            CheckInput();
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

    void CheckInput()
    {
        foreach (var item in Keys)
        {
            if (Input.GetKeyDown(item.Value) && item.Key == order[count])
            {
                Debug.Log("Correct key " + item.Value);
                count++;
            }
            else if(Input.GetKeyDown(item.Value))
            {
                Debug.Log("Incorrect key " + item.Value);
                incorrectKey = true;
            }
        }
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
