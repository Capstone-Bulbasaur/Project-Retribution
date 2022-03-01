using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Created script from this tutorial: https://www.youtube.com/watch?v=_Z1t7MNk0c4
     */

public class IsarrProjectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;
      

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    void Update()
    {
        

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }


}
