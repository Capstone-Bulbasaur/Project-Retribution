using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FFFire : MonoBehaviour
{
    public GameObject brokenWindow;
    public float spawnTime = 2;
    

    private int fireLevel = 0;
    private float upTimer;
    private bool isActive = true;


    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

        upTimer += Time.deltaTime;

        if (upTimer >= 3.0f && fireLevel < 2)
        {
            LevelUpFire();
            upTimer = 0;
        }
        else if (fireLevel == 2 && upTimer >= 5.0f && isActive == true)
        {
            

            transform.parent.gameObject.SetActive(false);
            //window break animation
            ExtinguishFire(1);
            isActive = false;
            upTimer = 0;
            //counter for broken windows logic now
            FFGameManager.instance.brokenWindows++;
                    
        }
        
    }

    public void ExtinguishFire(int sumthin = 0)//don't touch sumthin.
    {
        //Sound Effect?
        //Sploosh?

        Destroy(gameObject);
        
        if (sumthin == 0)
        {
            FFGameManager.instance.RemoveFlame();
        }
        else
        {
            GameObject broken = Instantiate(brokenWindow);
            broken.transform.position = new Vector3(gameObject.transform.position.x + 0.05f, gameObject.transform.position.y - 2.2f, gameObject.transform.position.z);
        }
        
        
    }

    public void LevelUpFire() //TODO after LevelUp: more user feedback for fire levels, maybe color change?
    {
        //can debate Sound Effect for level up if we think another differt Fywooosh would be needed.
        
        if(fireLevel <= 2)
        {
            fireLevel++; // up to Level2 which maxLevel could be a variable maybe?
            //scales the fire up on x&y
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 0.8f,
                gameObject.transform.localScale.y + 0.8f, 1);
            //Should adjust the ypos to compensate for the scaling
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.2f, gameObject.transform.position.z);

        }
    }

    public void WaterDownFire() 
    {
        //check for fire level
        if(fireLevel == 0)
        {
            ExtinguishFire();
        }
        else
        {
            fireLevel--;
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.8f,
                gameObject.transform.localScale.y - 0.8f, 1);
            //Should adjust the ypos to compensate for the scaling
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1.2f, gameObject.transform.position.z);
        }
        //Sound Effect?
        //SizzleDizzle?

    }
}
