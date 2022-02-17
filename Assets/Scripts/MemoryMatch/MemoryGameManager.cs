using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameObject = UnityEngine.GameObject;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using TMPro;
/*
 * FOLLOWED THIS TUTORIAL: https://www.youtube.com/watch?v=qaCjBh7bWz0&list=PLZhNP5qJ2IA2DA4bzDyxFMs8yogVQSrjW&ab_channel=AwesomeTuts and adapted to our game
 */


public class MemoryGameManager : MonoBehaviour
{
    [SerializeField] private Sprite back;
    [SerializeField] private Sprite[] matched;
    private bool firstGuess, secondGuess;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int countFails;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPot, secondGuessPot;

    public List<Button> btns = new List<Button>();
    public Sprite[] pots;
    public List<Sprite> gamePots = new List<Sprite>();
    public TextMeshProUGUI PlayerScore;
    public TextMeshProUGUI PlayerMissed;

    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePots();
        Shuffle(gamePots);
        CountGameGuesses();

        FindObjectOfType<AudioManager>().Play("Memory_Music");
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        //Add all button components to a list of buttons
        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = back;
        }
    }

    void AddListeners()
    {
        //Add the PickAPot function to the onClick of each Button
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPot());
        }
    }

    void AddGamePots()
    {
        int looper = btns.Count;
        int index = 0;

        //Adds 2 of every pot in the gamePots list
        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }

            gamePots.Add(pots[index]);
            index++;
        }
    }

    public void PickAPot()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        //Check to see if a pot is selected
        if (!firstGuess)
        {
            /*
             *INSERT SOUNDS HERE FOR SELECTING A POT
             */
            FindObjectOfType<AudioManager>().Play("Memory_SelectPot");

            firstGuess = true;
            firstGuessIndex = int.Parse(name);
            firstGuessPot = gamePots[firstGuessIndex].name;

            //Turn the sprite of the button to the corresponding pot image
            btns[firstGuessIndex].image.sprite = gamePots[firstGuessIndex];

            //Make buttons no longer interactable
            btns[firstGuessIndex].interactable = false;

        }else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(name);
            secondGuessPot = gamePots[secondGuessIndex].name;

            //Turn the sprite of the button to the corresponding pot image
            btns[secondGuessIndex].image.sprite = gamePots[secondGuessIndex];

            //Make button no longer interactable
            btns[secondGuessIndex].interactable = false;

            //Increment number of guesses 
            countGuesses++;

            StartCoroutine(CheckIfThePotsMatch());
        }
    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;
        PlayerScore.text = countCorrectGuesses.ToString();
        if (countCorrectGuesses == gameGuesses)
        {
            //Play sound for completing the game
            //SoundManager.Instance.PlayOneShot(SoundManager.Instance.winGameSound);
            FindObjectOfType<AudioManager>().Play("Memory_Win");
            
            Debug.Log("Game Finished!");
            Debug.Log("It took you " + countGuesses + " guess(es) to finish the game");

            FindObjectOfType<AudioManager>().StopPlaying("Memory_Music");

            SceneManager.LoadScene(sceneName: "HubWorld");
            
            //RestartGame();
        }
    }

    IEnumerator CheckIfThePotsMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstGuessPot == secondGuessPot)
        {
            //yield return new WaitForSeconds(.5f);

            //Make buttons no longer interactable
            //btns[firstGuessIndex].interactable = false;
           // btns[secondGuessIndex].interactable = false;
            
            var result = btns[firstGuessIndex].image.sprite.name.Remove(0, 16);
            
            int index = int.Parse(result);
            index -= 1;

            //Play sound for getting a match right
            //SoundManager.Instance.PlayOneShot(SoundManager.Instance.rightMatchSound);
            FindObjectOfType<AudioManager>().Play("Memory_Correct");

            //Turn images to outlined version
            btns[firstGuessIndex].image.sprite = matched[index];
            btns[secondGuessIndex].image.sprite = matched[index];
            
            CheckIfTheGameIsFinished();
            Debug.Log("Its a MATCH!");
        }
        else
        {
            countFails++;

            PlayerMissed.text = countFails.ToString();

            //Play sound for getting a match wrong
            FindObjectOfType<AudioManager>().Play("Memory_Wrong");

            //Turn images to blank version 
            btns[firstGuessIndex].image.sprite = back;
            btns[secondGuessIndex].image.sprite = back;

            //Make First and second pot interactable again
            btns[firstGuessIndex].interactable = true;
            btns[secondGuessIndex].interactable = true;

            //When its restarting its not restarting the correct pots
            if (countFails >= 5)
            {
                RestartGame();
            }

            Debug.Log("Its NOT a MATCH!");
        }

        yield return new WaitForSeconds(.5f);

        //Reset Selected Pots
        firstGuess = secondGuess = false;
    }

    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void CountGameGuesses()
    {
        //Divide the amount of pots by 2 to get the number of guesses you need to finish the game
        gameGuesses = gamePots.Count / 2;
    }

    void RestartGame()
    {
        //Loop through buttons to make them interactable and change the sprite to empty pot
        foreach (Button btn in btns)
        {
            btn.image.sprite = back;
            btn.interactable = true;
        }

        Shuffle(gamePots);
        CountGameGuesses();

        //Reset Guesses
        countCorrectGuesses = countGuesses = countFails = 0;

        PlayerMissed.text = countFails.ToString();
        PlayerScore.text = countCorrectGuesses.ToString();
    }
}
