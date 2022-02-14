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

    Vector2 movement;
    private Vector2 boxSize = new Vector2(2f, 1f); // Need this box size for raycasting to find interactable objects around the player


    private void Start()
    {
        interactIcon.SetActive(false);
        animator = GetComponent<Animator>();
    }
    // Handles user input
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }
    }

    // Handles movement
    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement.normalized * speed * Time.fixedDeltaTime); // * Time.fixedDeltaTime to get constant movement speed
    }

    public void OpenInteractBubble()
    {
        interactIcon.SetActive(true);
    }

    public void CloseInteractBubble()
    {
        interactIcon.SetActive(false);
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
