using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField] private Transform puzzleZone;
    [SerializeField] private GameObject btn;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i; //Rename Pot Prefabs
            button.transform.SetParent(puzzleZone, false);
        }
    }

}
