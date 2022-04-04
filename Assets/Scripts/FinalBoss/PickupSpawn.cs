using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] pickups;
    private int pickupsOnScreen;

    void SpawnPickups()
    {
        GameObject pickup = Instantiate(pickups[Random.Range(0, pickups.Length)], transform, true);
        pickup.transform.position = transform.position;

        //GameObject pickup = pickups[Random.Range(0, pickups.Length)];
        //bool spawned = false;

        ////Check if there's already 18 fires on screen
        //if (pickupsOnScreen < pickups.Length)
        //{
        //    while (!spawned)
        //    {
        //        if (spawnLocation.transform.childCount == 0)
        //        {
        //            GameObject fires = Instantiate(flame, spawnLocation.gameObject.transform, false);
        //            //Sound Effect?
        //            //Fwoosh?
        //            fires.transform.position = spawnLocation.transform.position;
        //            flamesOnScreen += 1;
        //            spawned = true;
        //        }
        //        else
        //        {
        //            spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //        }
        //    }
        //}
    }

    IEnumerator respawnPickup()
    {
        yield return new WaitForSeconds(20);
        SpawnPickups();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPickups();
    }

    public void PickupWasPickedUp()
    {
        StopAllCoroutines();
        StartCoroutine("respawnPickup");
        //Invoke("SpawnPickups", 20f);
    }
}
