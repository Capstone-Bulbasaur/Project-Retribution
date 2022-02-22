using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isarr : MonoBehaviour
{
    private float health;
    private float fireRate;
    private float speed;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

}
