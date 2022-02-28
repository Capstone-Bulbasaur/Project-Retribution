using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Created script from this tutorial: https://www.youtube.com/watch?v=lHLZxd0O6XY
     */

public class IsarrProjectile : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D IsarrProjectileRB;

    void Start()
    {
        IsarrProjectileRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        IsarrProjectileRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2);
        
    }

    
}
