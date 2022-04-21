using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FFFire : MonoBehaviour
{
    public GameObject brokenWindow;
    public float spawnTime = 2;
    public GameObject particleObject;
    public GameObject particleParent;
    
    public Sprite levelZero;
    public Sprite levelOne;
    public Sprite levelTwo;

    public RuntimeAnimatorController levelZeroAnim;
    public RuntimeAnimatorController levelOneAnim;
    public RuntimeAnimatorController levelTwoAnim;


    private int fireLevel = 0;
    private float upTimer;
    private bool isActive = true;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    


    void Start()
    {
        particleParent = GameObject.FindGameObjectWithTag("PuzzleButton");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //if(FFGameManager.instance.gameOver)
        //{
        //    ExtinguishFire();
        //}
        //this logic might need to move, not sure if it matters - Steve


        if (FFUIManager.instance.currentTime >= 5.0f)
        {
            upTimer += Time.deltaTime;

            if (upTimer >= 2.0f && fireLevel < 2)
            {
                LevelUpFire();
                upTimer = 0;
            }
            else if (fireLevel == 2 && upTimer >= 4.0f && isActive == true)
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

    }

    public void ExtinguishFire(int sumthin = 0)//don't touch sumthin.
    {
        //Sound Effect?
        //Sploosh?
        
        Destroy(gameObject);

        //If sumthin = 0 then the fire was put out by the player, otherwise it reached max level and broke a window
        if (sumthin == 0)
        {
            AudioManager.instance.Play("Fire_Splash");
        }
        else
        {
            GameObject broken = Instantiate(brokenWindow);
            var position = gameObject.transform.position;
            broken.transform.position = new Vector3(position.x + 0.05f, position.y - 2.2f, position.z);

            AudioManager.instance.Play("Fire_Break");
        }
        FFGameManager.instance.RemoveFlame();
    }

    public void LevelUpFire() //TODO after LevelUp: more user feedback for fire levels, maybe color change?
    {
        //can debate Sound Effect for level up if we think another differt Fywooosh would be needed.

        if(fireLevel <= 2)
        {
            fireLevel++; // up to Level2 which maxLevel could be a variable maybe?
            
            if (fireLevel == 1)
            {
                spriteRenderer.sprite = levelOne;
                animator.runtimeAnimatorController = levelOneAnim;
            }
            else if (fireLevel == 2)
            {
                spriteRenderer.sprite = levelTwo;
                animator.runtimeAnimatorController = levelTwoAnim;
            }

            //To scale up pass in 1, to scale down pass in -1
            ScaleFires();
        }
    }

    public void WaterDownFire()
    {
        //Play particle effect 
        GameObject particles = Instantiate(particleObject, particleParent.transform);
        particles.transform.position = gameObject.transform.position;

        //check for fire level
        if(fireLevel == 0)
        {
            ExtinguishFire();
            //AudioManager.instance.Play("Fire_PutOut");
        }
        else
        {
            fireLevel--;
            if (fireLevel == 0)
            {
                spriteRenderer.sprite = levelZero;
                animator.runtimeAnimatorController = levelZeroAnim;
            }
            else if (fireLevel == 1)
            {
                spriteRenderer.sprite = levelOne;
                animator.runtimeAnimatorController = levelOneAnim;
            }
            
            //To scale up pass in 1, to scale down pass in -1
            ScaleFires(-1);
        }
        //Sound Effect?
        //SizzleDizzle?
    }

    void ScaleFires(short direction = 1)
    {
        //scales the fire up on x&y
        var localScale = gameObject.transform.localScale;
        localScale = new Vector3(localScale.x + (0.8f * direction), localScale.y + (0.8f * direction), 1);
        gameObject.transform.localScale = localScale;

        //Should adjust the ypos to compensate for the scaling
        var position = gameObject.transform.position;
        position = new Vector3(position.x, position.y + (1.2f * direction), position.z);
        gameObject.transform.position = position;
    }
}
