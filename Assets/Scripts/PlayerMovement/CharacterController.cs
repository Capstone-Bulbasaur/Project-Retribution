using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rigidbody;
    public Animator animator;

    Vector2 movement;

    // Handles user input
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    // Handles movement
    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement.normalized * speed * Time.fixedDeltaTime); // * Time.fixedDeltaTime to get constant movement speed

    }
}
