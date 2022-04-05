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
    private TextMeshProUGUI YouLose;

    public List<Button> btns = new List<Button>();
    public Sprite[] pots;
    public List<Sprite> gamePots = new List<Sprite>();
    public TextMeshProUGUI PlayerScore;
    public TextMeshProUGUI PlayerMissed;

    public float PlayerXpos;
    public float PlayerYpos;

    public GameObject youLosePanel;

    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePots();
        Shuffle(gamePots);
        CountGameGuesses();

        youLosePanel.SetActive(false);
        
        AudioManager.instance.Play("Memory_Music");
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
            AudioManager.instance.Play("Memory_SelectPot");

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

    IEnumerator CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;
        PlayerScore.text = countCorrectGuesses.ToString();
        
        if (countCorrectGuesses == gameGuesses)
        {
            //Play sound for completing the game
            AudioManager.instance.Play("Memory_Win");

            yield return new WaitForSeconds(1.5f);

            Debug.Log("Game Finished!");
            Debug.Log("It took you " + countGuesses + " guess(es) to finish the game");

            AudioManager.instance.StopPlaying("Memory_Music");


            //Recruited orry
            //MainGameManager.instance.RecruitAlly(Constants.Orry);
            PlayerPrefs.SetInt("RecruitedOrry", 1);

            //SceneManager.LoadScene(sceneName: "MM-Midscene-YouWin");
            LevelChanger.instance.FadeToLevel((int)Constants.gameScenes.MEMMATCHWIN);
        }
    }
    

    IEnumerator CheckIfThePotsMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstGuessPot == secondGuessPot)
        {
            //Remove the first part of the sprite name leaving only the number at the end.
            var result = btns[firstGuessIndex].image.sprite.name.Remove(0, 16);
            
            int index = int.Parse(result);
            index -= 1;

            //Play sound for getting a match right
            AudioManager.instance.Play("Memory_Correct");

            //Turn images to outlined version
            btns[firstGuessIndex].image.sprite = matched[index];
            btns[secondGuessIndex].image.sprite = matched[index];
            
            StartCoroutine(CheckIfTheGameIsFinished());
        }
        else
        {
            countFails++;

            PlayerMissed.text = countFails.ToString();

            //Play sound for getting a match wrong
            AudioManager.instance.Play("Memory_Wrong");

            //Turn images to blank version 
            btns[firstGuessIndex].image.sprite = back;
            btns[secondGuessIndex].image.sprite = back;

            //Make First and second pot interactable again
            btns[firstGuessIndex].interactable = true;
            btns[secondGuessIndex].interactable = true;

            //When its restarting its not restarting the correct pots
            if (countFails >= 5)
            {
                // got part of the Lose panel solution on this tutorial: https://www.youtube.com/watch?v=e0feEWLRSYI
                youLosePanel.gameObject.SetActive(true); // make the youLosePanel visible if is the 5th fail
                yield return new WaitForSeconds(2.0f); // wait for 2s
                youLosePanel.gameObject.SetActive(false); // make the youLosePanel invisible again before the Restart Game

                RestartGame();
            }
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

    public void RetrunMainMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
        Time.timeScale = 1f;
    }
}
