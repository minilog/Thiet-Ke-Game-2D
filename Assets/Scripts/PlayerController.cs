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
    private bool readyForDoubleJump;
    public float FlyKichPrepare;
    public float FlyKichAttack;
    public float StrikePrepareTranslate;
    public float StrikeAttackTranslate;
    public float StrikeTime;
    public float AttackDamage { get; private set; }
    [Space]
    // Keycode
    public KeyCode JumpKeyCode,
                   CrouchKeyCode,
                   AttackKeyCode,
                   StrikeKeyCode,
                   FlyKickKeyCode;

    // Use for animation
    float horizontalAxis;

    // About Flip
    public bool isFacingRight = true;
        
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
                rb2D.AddForce(new Vector2(0f, JumpForce));
            }
        }
        else if (readyForDoubleJump)
        {
            if (Input.GetKeyDown(JumpKeyCode))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump Attack"))
                    animator.Play("Player Jump", 0, 0f);
                readyForDoubleJump = false;
                rb2D.velocity = new Vector2(0, 0);
                rb2D.AddForce(new Vector2(0f, JumpForce));
            }
        }

        // Check Flip game Object
        CheckFlip();

        // Set Animation Parameters
        animator.SetBool("Crouching", Input.GetKey(CrouchKeyCode));
        animator.SetBool("IsGrounded", checkGround.IsGrounded);
        animator.SetBool("Walking", horizontalAxis != 0);
        animator.SetBool("Attack", Input.GetKey(AttackKeyCode));
        animator.SetBool("Strike", Input.GetKeyDown(StrikeKeyCode));
        animator.SetBool("FlyKick", Input.GetKeyDown(FlyKickKeyCode));

        // On Character State
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Walk"))
            transform.Translate(new Vector3(horizontalAxis * WalkSpeed * Time.deltaTime, 0, 0));
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Idle"))
        { }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump"))
            transform.Translate(new Vector3(horizontalAxis * JumpSpeed * Time.deltaTime, 0, 0));
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Crouch"))
        { }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Attack"))
        { }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump Attack"))
            transform.Translate(new Vector3(horizontalAxis * JumpSpeed * Time.deltaTime, 0, 0));
        // movement of Strike Animation is too difficult, so I put them in Behavior

    }

    private void CheckFlip()
    {
        if (horizontalAxis < 0 && isFacingRight)
        {
            isFacingRight = false;
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
        else if (horizontalAxis > 0 && !isFacingRight)
        {
            isFacingRight = true;
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