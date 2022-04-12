using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPlayerLocation : MonoBehaviour
{
    public GameObject Limecoast;
    public GameObject Baygate;
    public GameObject Dragongulf;
    public GameObject Silkcross;
    public GameObject Minion;

    public void ChangePlayerLocation(int location)
    {
        switch (location)
        {
            case (int)Constants.minimapTowns.LIMECOAST:
               Debug.Log("You are in Limecoast");
               break;
            case (int)Constants.minimapTowns.BAYGATE:
                Debug.Log("You are in Baygate");
                break;
            case (int)Constants.minimapTowns.DRAGONGULF:
                Debug.Log("You are in Dragongulf");
                break;
            case (int)Constants.minimapTowns.SILKCROSS:
                Debug.Log("You are in Silkcross");
                break;
            case (int)Constants.minimapTowns.MINION:
                Debug.Log("You are with the minion");
                break;
            default:
                Debug.LogError("Bad location type passed" + location);
                break;
        
        }
    }
}
