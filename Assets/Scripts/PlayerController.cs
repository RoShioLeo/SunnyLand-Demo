using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public LayerMask ground;
    public Collider2D playerCollider;
    public Collider2D croachCollider;
    public Text cherryNumber;
    public AudioSource jumpAudio, hurtAudio, cherryAudio;
    public Transform cell;

    public float speed, jumpForce;
    public int jumpCount, maxJumpCount, cherry;
    public bool isJumpKeyDown = false, isJump = false, isGround = true;

    private int hurtTick = 0;

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
        Crouch();
    }

    void FixedUpdate()
    {
        GroundMove();
        JudgeToJump();
        OnFallen();
        TickHurt();
    }

    void GroundMove()
    {
        if (!animator.GetBool("hurting"))
        {
            float direction = Input.GetAxisRaw("Horizontal");
            animator.SetFloat("running", Mathf.Abs(direction));
            rb.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, rb.velocity.y);
            if (direction > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    void Jump()
    {
        isJump = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpAudio.Play();
        animator.SetBool("jumping", true);
        jumpCount--;
        isJumpKeyDown = false;
    }

    void JudgeToJump()
    {
        if (isGround)
        {
            jumpCount = maxJumpCount;
            isJump = false;
        }
        if (isJumpKeyDown && isGround)
        {
            Jump();
        }
        else if (isJumpKeyDown && jumpCount > 0 && isJump)
        {
            Jump();
        }
    }

    void Crouch()
    {
        if (!Physics2D.OverlapCircle(cell.position, 0.2F, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                animator.SetBool("crouching", true);
                playerCollider.enabled = false;
                croachCollider.enabled = true;
            }
            else
            {
                animator.SetBool("crouching", false);
                playerCollider.enabled = true;
                croachCollider.enabled = false;
            }
        }
        
    }

    void OnFallen()
    {
        if (!playerCollider.IsTouchingLayers(ground) && !croachCollider.IsTouchingLayers(ground))
        {
            if (rb.velocity.y < 0)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
                isGround = false;
            }
        }
        else
        {
            animator.SetBool("falling", false);
            isGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Collection")
        {
            Destroy(collider.gameObject);
            cherry++;
            cherryNumber.text = "x " + cherry;
            cherryAudio.Play();
        }
        else if (collider.tag == "Deadline")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Reload", 1);
        }
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (animator.GetBool("falling"))
            {
                enemy.Kill();
                Jump();
            }
            else
            {
                animator.SetBool("hurting", true);
                hurtAudio.Play();
                isJump = true;
                rb.velocity = new Vector2(-rb.velocity.x, jumpForce);
                animator.SetBool("jumping", true);
                jumpCount = 0;
                isJumpKeyDown = false;
            }
        }
    }

    private void TickHurt()
    {
        if (animator.GetBool("hurting"))
        {
            hurtTick++;
        }
        if (hurtTick > 40)
        {
            hurtTick = 0;
            animator.SetBool("hurting", false);
        }
    }
}
