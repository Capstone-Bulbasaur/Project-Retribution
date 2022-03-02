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
    private float range;
    private Vector3 shootDir;

    IEnumerator deathTimer()
    {
        //Destroy the object after its reached its max range
        yield return new WaitForSeconds(range);
        Destroy(transform.parent.gameObject);
    }

    
    //Calculate the angle the object needs to face
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        //Minus 90 because the sprite is originally at the 90 degree position instead of 0 degree
        return n - 90;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        Vector3 shootDirection = new Vector3(player.position.x, player.position.y, 0);
        shootDirection.Normalize();
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDirection));
    }

    void Update()
    {
        float moveSpeed = 10f;
        //Move the projectile towards the shooting direction
        transform.position += shootDir * (moveSpeed * Time.deltaTime);
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
