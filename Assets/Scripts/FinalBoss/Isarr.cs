using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isarr : MonoBehaviour
{
    public float health;
    public float fireRate;
    public float speed;
    public Animator animator;
    public GameObject target;

    //[MS] - double check later if we really need, as we already have the general SoundManager
    private AudioSource audioSource;

    bool CanSeeTarget() // [MS] To be tested - got it from AI lab, Cop and robber, adapted to 2d (changed the vector3 to vector2)
    {
        RaycastHit raycastInfo;
        Vector2 rayToTarget = target.transform.position - this.transform.position;
        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            if (raycastInfo.transform.gameObject.tag == "Graey")
                return true;
        }
        return false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

}
