using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFFire : MonoBehaviour
{

    public float spawnTime = 2;
    
    private int fireLevel = 0; 
    

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ExtinguishFire() //man, does this ever work but like, it can't be the best way.
    {
        //Sound Effect?
        //Sploosh?
        
        Debug.Log("Clicked Fire" + gameObject);

        Destroy(gameObject); //to be this lazy would involve checks for what level the fire is, confirm level 0 on click

        FFGameManager.instance.RemoveFlame();
    }

    public void LevelUpFire() //Level 0 fire -> Level 1 fire after say 2 seconds without click then Level 1 fire -> Level 2 after 3 seconds maybe?
    {
        //can debate Sound Effect for level up if we think another differt Fywooosh would be needed.
        //needs time logic and checks for what level the fires are
        fireLevel++; // up to Level2 which I guess maxLevel could be a variable maybe?
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
        
        //Sound Effect?
        //SizzleDizzle?

        fireLevel--;
        //reverse logic of levelup
        //lvl2->lvl1 scale - .25
        //lvl1->lvl0 scale - .25
        //lvl2->lvl1 position.y - .17
        //lvl1->lvl0 position.y - .15
    }
}
