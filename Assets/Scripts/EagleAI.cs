using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAI : Enemy
{
    private Rigidbody2D rb;
    private float topY, bottomY;
    public Transform top, bottom;
    public float speed;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        topY = top.position.y;
        bottomY = bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
        rb.velocity = new Vector2(0, speed);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (topY < transform.position.y || rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(0, -speed);
        }
        if (bottomY > transform.position.y || rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(0, speed);
        }
    }
}
