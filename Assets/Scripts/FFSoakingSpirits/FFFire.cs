using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FFFire : MonoBehaviour
{

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
        if (gameObject.activeInHierarchy == true)
        {
         
            upTimer += Time.deltaTime;

            if (upTimer >= 3.0f && fireLevel < 2)
            {
                LevelUpFire();
                upTimer = 0;
            }
            else if (fireLevel == 2 && upTimer >= 5.0f && isActive == true)
            {
                //Debug.Log("Window's crashed");
                transform.parent.gameObject.SetActive(false);
                ExtinguishFire(1);
                isActive = false;
                upTimer = 0;
                
                //window break animation and counter for broken windows logic now
            }
        }
    }

    public void ExtinguishFire(int sumthin = 0) //man, does this ever work but like, it can't be the best way.
    {
        //Sound Effect?
        //Sploosh?
        
        //Debug.Log("Clicked Fire" + gameObject);

        Destroy(gameObject); //to be this lazy would involve checks for what level the fire is, confirm level 0 on click
        
        if (sumthin == 0)
        {
            FFGameManager.instance.RemoveFlame();
        }
    }

    public void LevelUpFire() //Level 0 fire -> Level 1 fire after say 2 seconds without click then Level 1 fire -> Level 2 after 3 seconds maybe?
    {
        //can debate Sound Effect for level up if we think another differt Fywooosh would be needed.
        //needs time logic and checks for what level the fires are
        
        if(fireLevel <= 2)
        {
            fireLevel++; // up to Level2 which I guess maxLevel could be a variable maybe?
            //scales the fire up on x&y
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 0.5f,
                gameObject.transform.localScale.y + 0.5f, 1);
            //Should adjust the ypos to compensate for the scaling
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f, gameObject.transform.position.z);

        }
        //will need logic for size scaling fires.transform.scale(?)
        //Level1 X&Y scale + 0.25
        //Level2 X&Y scale + 0.25 (so should be .5 scaled up from oglvl0)
        //the same logic needs to adjust the fires.transform.position.y
        //level1 +.15
        //level2 +.17 (should be .32 up from oglvl0)
    }

    public void WaterDownFire() //Make sure fire is above 0
    {
        //this would be called on click if fire is above 0
        if(fireLevel == 0)
        {
            ExtinguishFire();
        }
        else
        {
            fireLevel--;
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.5f,
                gameObject.transform.localScale.y - 0.5f, 1);
            //Should adjust the ypos to compensate for the scaling
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1.0f, gameObject.transform.position.z);


        }
        //Sound Effect?
        //SizzleDizzle?


        //reverse logic of levelup
        //lvl2->lvl1 scale - .25
        //lvl1->lvl0 scale - .25
        //lvl2->lvl1 position.y - .17
        //lvl1->lvl0 position.y - .15
    }
}
