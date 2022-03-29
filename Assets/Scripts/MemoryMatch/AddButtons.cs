using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField] private Transform puzzleZone;
    [SerializeField] private GameObject btn;
    public int numOfPots = 6;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < numOfPots; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i; //Rename Pot Prefabs
            button.transform.SetParent(puzzleZone, false);
        }
    }

}
