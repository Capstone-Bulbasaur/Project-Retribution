using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[MS] - got this tutorial from https://www.youtube.com/watch?v=1vVdCXjXja4 and adapted to our game

public class MainPot : MonoBehaviour
{
    public Sprite[] faces;
    public Sprite back;
    public int faceIndex;
    public bool matched = false;

    SpriteRenderer spriteRenderer;

    private GameObject gameControl;

    public void OnMouseDown()
    {
        if (matched == false)
        {
            if (spriteRenderer.sprite == back)
            {
                //if 2 pots flipped = false, turn the sprite and addVisibleFace
                if (gameControl.GetComponent<MMGameControl>().TwoPotsFlipped() == false)
                {
                    spriteRenderer.sprite = faces[faceIndex];
                    gameControl.GetComponent<MMGameControl>().AddVisibleFace(faceIndex);
                    matched = gameControl.GetComponent<MMGameControl>().CheckMatch(); // if this match is true, the player will not longer flip the pot over again
                }
            }
            else
            {
                spriteRenderer.sprite = back;
                gameControl.GetComponent<MMGameControl>().RemoveVisibleFace(faceIndex);
            }
        }
    }

    private void Awake()
    {
        gameControl = GameObject.Find("MMGameManager");
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
