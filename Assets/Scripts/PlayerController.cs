using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public LayerMask ground;
    public Collider2D playerCollider;
    public Text cherryNumber;

    public float speed, jumpForce;
    public int jumpCount, maxJumpCount, cherry;
    public bool isJumpKeyDown = false, isJump = false, isGround = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumpKeyDown = true;
        }
    }

    void FixedUpdate()
    {
        GroundMove();
        Jump();
        OnFallen();
    }

    void GroundMove()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("running", Mathf.Abs(direction));
        rb.velocity = new Vector2(direction * speed * Time.deltaTime, rb.velocity.y);
        if (direction != 0)
        {
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    void Jump()
    {
        if (isGround)
        {
            jumpCount = maxJumpCount;
            isJump = false;
        }
        if(isJumpKeyDown && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("jumping", true);
            jumpCount--;
            isJumpKeyDown = false;
        }
        else if (isJumpKeyDown && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("jumping", true);
            jumpCount--;
            isJumpKeyDown = false;
        }
    }

    void OnFallen()
    {
        if (animator.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
            }
        }
        else if (playerCollider.IsTouchingLayers(ground))
        {
            animator.SetBool("falling", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Collection")
        {
            Destroy(collider2D.gameObject);
            cherry++;
            cherryNumber.text = "x " + cherry;
        }
    }
}
