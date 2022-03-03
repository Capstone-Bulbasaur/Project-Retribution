using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public int type;

    //private void OnTriggerEnter(Collider collider)
    //{
    //    Debug.Log("Picked Up!");
    //    if (collider.gameObject.tag == "Player")
    //    {
    //        collider.gameObject.GetComponent<Graey>().PickupItem(type);
    //        GetComponentInParent<PickupSpawn>().PickupWasPickedUp();
    //        Destroy(gameObject);
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collider)
    //{
    //    Debug.Log("Picked Up!");
    //    if (collider.gameObject.tag == "Player")
    //    {
    //        //TODO ADD SOUND HERE
    //        collider.gameObject.GetComponent<PowerSwitcher>().PickupItem(type);
    //        GetComponentInParent<PickupSpawn>().PickupWasPickedUp();
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Picked Up!");
        if (collider.gameObject.CompareTag("Player"))
        {
            //TODO ADD SOUND HERE
            collider.gameObject.GetComponent<PowerSwitcher>().PickupItem(type);
            GetComponentInParent<PickupSpawn>().PickupWasPickedUp();
            Destroy(gameObject);
        }
    }
}
