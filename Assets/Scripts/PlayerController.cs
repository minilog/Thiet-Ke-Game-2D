using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movement & Animation
public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] Animator animator;
    [SerializeField] CheckGround checkGround;
    [SerializeField] BoxCollider2D normalBoxCollider;
    [SerializeField] BoxCollider2D crouchBoxCollider;
    public GameObject FireBallPrefab;
    [Space]
    public float WalkSpeed;
    public float JumpForce;
    public float JumpSpeed;
    public float Gravity;
    private bool readyForDoubleJump;
    public float AttackDamage { get; private set; }

    [Space]
    // Keycode
    public KeyCode JumpKeyCode,
                   CrouchKeyCode,
                   AttackKeyCode;

    // Use for animation
    float horizontalAxis;

    // About Change State
        
    private void OnValidate()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.speed = 0.66f;
    }

    private void Update()
    {
        // Time Scale
        if (Input.GetKey(KeyCode.Q))
            Time.timeScale = 0.1f;
        else
            Time.timeScale = 1f;

        horizontalAxis = Input.GetAxis("Horizontal");

        // Check Ground && Jump
        if (checkGround.IsGrounded)
        {
            readyForDoubleJump = true;
            if (Input.GetKeyDown(JumpKeyCode))
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, JumpForce);
            }
        }
        else if (readyForDoubleJump)
        {
            if (Input.GetKeyDown(JumpKeyCode))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump Attack"))
                    animator.Play("Player Jump", 0, 0f);
                readyForDoubleJump = false;
                rb2D.velocity = new Vector2(rb2D.velocity.x, JumpForce);
            }
        }

        // Check Flip game Object
        CheckFlip();

        // Apply gravity
        rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y - Gravity * Time.deltaTime);

        // Set Animation Parameters
        animator.SetBool("Crouching", Input.GetKey(CrouchKeyCode));
        animator.SetBool("IsGrounded", checkGround.IsGrounded);
        animator.SetBool("Walking", horizontalAxis != 0);
        animator.SetBool("Attack", Input.GetKey(AttackKeyCode));

        // On Character State
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Walk"))
            rb2D.velocity = new Vector2(horizontalAxis * WalkSpeed * Time.deltaTime, rb2D.velocity.y);
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Idle"))
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump"))
            rb2D.velocity = new Vector2(horizontalAxis * JumpSpeed * Time.deltaTime, rb2D.velocity.y);
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Crouch"))
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Attack"))
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump Attack"))
            rb2D.velocity = new Vector2(horizontalAxis * JumpSpeed * Time.deltaTime, rb2D.velocity.y);
    }

    private void CheckFlip()
    {
        if (horizontalAxis < 0 && transform.localScale.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
        else if (horizontalAxis > 0 && transform.localScale.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
    } 

    // Use for Crouching
    public void EnableNormalBoxCollider()
    {
        normalBoxCollider.enabled = true;
        crouchBoxCollider.enabled = false;
    }

    public void EnableCrouchBoxCollider()
    {
        normalBoxCollider.enabled = false;
        crouchBoxCollider.enabled = true;
    }
}