using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : Enemy
{
    private Rigidbody2D rb;
    public LayerMask ground;
    public Collider2D frogCollider;

    public Transform left, right;
    public float speed, jumpForce;

    private float leftX, rightX;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        leftX = left.position.x;
        rightX = right.position.x;
        Destroy(left.gameObject);
        Destroy(right.gameObject);
    }

    void FixedUpdate()
    {
        OnFallen();
    }

    void Jump()
    {
        if (transform.localScale.x == 1)
        {
            if (leftX < transform.position.x)
            {
                rb.velocity = new Vector2(-speed, jumpForce);
                animator.SetBool("jumping", true);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                rb.velocity = new Vector2(speed, jumpForce);
                animator.SetBool("jumping", true);
            }
        }
        else if (transform.localScale.x == -1)
        {
            if (rightX > transform.position.x)
            {
                rb.velocity = new Vector2(speed, jumpForce);
                animator.SetBool("jumping", true);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                rb.velocity = new Vector2(-speed, jumpForce);
                animator.SetBool("jumping", true);
            }
        }
    }

    void OnFallen()
    {
        if (animator.GetBool("jumping"))
        {
            if (rb.velocity.y <= 0)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
            }
        }
        else if (frogCollider.IsTouchingLayers(ground))
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);
        }
    }
}
