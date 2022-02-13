using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

//[MS] - got this tutorial from https://www.youtube.com/watch?v=1vVdCXjXja4 and adapted to our game

public class MMGameControl : MonoBehaviour
{
    GameObject emptyPot;
    private List<int> faceIndexes = new List<int> { 0, 1, 2, 0, 1, 2 };
    public static System.Random rnd = new System.Random();
    public int shuffleNum = 0;

    private int[] visibleFaces = { -1, -2 };

    void Start()
    {
        int originalLenght = faceIndexes.Count;
        float yPosition = 3.3f;
        float xPosition = -3.2f;

        for (int i = 0; i < 6; i++)
        {
            //shuffling randomly according the sprites sequences we have on the MainPot Script and on EmptyPot obj (faces)
            shuffleNum = rnd.Next(0, (faceIndexes.Count));

            //instantiating new Pots on the screen
            var temp = Instantiate(emptyPot, new Vector3(
                                    xPosition, yPosition, 0), Quaternion.identity);
            
            temp.GetComponent<MainPot>().faceIndex = faceIndexes[shuffleNum];
            faceIndexes.Remove(faceIndexes[shuffleNum]);

            xPosition = xPosition + 3;
            if (i == originalLenght/2 -1)
            {
                yPosition = -3.3f;
                xPosition = -3.2f;
            }
        }

        emptyPot.GetComponent<MainPot>().faceIndex = faceIndexes[0];
    }

    public bool TwoPotsFlipped() // locking up when 2 pots are flipped
    {
        bool potsFlipped = false;

        if (visibleFaces[0] >= 0 && visibleFaces[1] >= 0)
        {
            potsFlipped = true;
        }

        return potsFlipped;

    }

    public void AddVisibleFace(int index) // 
    {
        if (visibleFaces[0] == -1) // if something is already visible = that particular index
        {
            visibleFaces[0] = index;

        }
        else if (visibleFaces[1] == -2) // if this is empty = that particular index
        {
            visibleFaces[1] = index;
        }
    }

    public void RemoveVisibleFace(int index) // removing visible faces when it is flipped over
    {
        if (visibleFaces[0] == index) 
        {
            visibleFaces[0] = -1;

        }
        else if (visibleFaces[1] == index) 
        {
            visibleFaces[1] = -2;
        }
    }

    public bool CheckMatch() // check if both visibleFaces indexes are equal
    {
        bool success = false;

        if (visibleFaces[0] == visibleFaces[1])
        {
            visibleFaces[0] = -1;
            visibleFaces[1] = -2;
            success = true;
        }

        return success;
    }


    void Awake()
    {
        emptyPot = GameObject.Find("EmptyPot");

    }

}
