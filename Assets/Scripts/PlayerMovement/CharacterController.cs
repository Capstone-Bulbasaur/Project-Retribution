// Followed a combination of https://www.youtube.com/watch?v=whzomFgjT50 and https://www.youtube.com/watch?v=fRpoE4FfJf8 to get the idle blendtree working so the character stays facing the last side they were moving towards
// Followed this tutorial to add interaction bubbles when there's an interactable NPC https://www.youtube.com/watch?v=GaVADPZlO0o

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject interactIcon;

    public float speed = 5f;
    public Rigidbody2D rigidbody;
    public Animator animator;
    public Joystick joystick;

    [HideInInspector]
    public AudioSource footsteps;

    Vector2 movement;
    private Vector2 boxSize = new Vector2(2.5f, 2.5f); // Need this box size for raycasting to find interactable objects around the player
    private DetectControlMethod controlMethod;
    [SerializeField] private GameObject leftStick;

    private void Start()
    {
        interactIcon.SetActive(false);
        animator = GetComponent<Animator>();

        footsteps = GetComponentInChildren<AudioSource>();

        controlMethod = GetComponent<DetectControlMethod>();
    }

    // Handles user input
    void Update()
    {
        // Action button
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }

        if (!controlMethod.usePhone)
        {
            leftStick.SetActive(false);
            // Character movement
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Set idle position to last known direction
            if (movement.x > 0 || movement.y > 0 || movement.x < 0 || movement.y < 0)
            {
                animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));

                footsteps.mute = false;
            }
        }

        if (controlMethod.usePhone)
        {
            leftStick.SetActive(true);
            if (joystick.Horizontal >= .2f || joystick.Horizontal <= -.2f)
                movement.x = joystick.Horizontal;
            else
                movement.x = 0;

            if (joystick.Vertical >= .2f || joystick.Vertical <= -.2f)
                movement.y = joystick.Vertical;
            else
                movement.y = 0;

            // Set idle position to last known direction
            if (movement.x != 0 || movement.y != 0)
            {
                
                animator.SetFloat("LastHorizontal", joystick.Horizontal);
                animator.SetFloat("LastVertical", joystick.Vertical);

                footsteps.mute = false;
            }
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x == 0 && movement.y == 0)
        {
            footsteps.mute = true;
        }
        
    }

    // Handles movement
    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement.normalized * speed * Time.fixedDeltaTime); // * Time.fixedDeltaTime to get constant movement speed
    }

    public void OpenInteractBubble()
    {
        interactIcon.SetActive(true); // Show exclamation bubble
    }

    public void CloseInteractBubble()
    {
        interactIcon.SetActive(false); // Hide exclamation bubble
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero); // Origin, size, angle and direction of the box

        if (hits.Length > 0)
        {
            foreach(RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }
}
