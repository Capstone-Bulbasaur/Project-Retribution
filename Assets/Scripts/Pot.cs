using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Pot : Image // [MF] inheritance does not make much sense but I want to access the Image component
{
    public int ID;
    public string[] Image; // [MF] is still a string but I want to call as an obj from Image class
    public bool isFlipped = false;
    public int playerChoice;
    public int cursor;
    public int[] Pots; // [MF] thinking to add as an array of ints to loop through the pots

    private Vector2 Position;

    private void Flipped()
    {
        if (cursor == playerChoice)
        {
            isFlipped = true;
        }
    }

    private void Choice()
    {

    }



}
