using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movement & Animation
public class PlayerController : MonoBehaviour
{
    // RIGIDBODY is always effected by Time.deltaTime
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] Animator animator;
    [SerializeField] CheckGround checkGround;
    [SerializeField] BoxCollider2D normalBoxCollider;
    [SerializeField] BoxCollider2D crouchBoxCollider;
    public GameObject FireBallPrefab;

    [Space]
    public float WalkSpeed;
    public float JumpVelocity;
    public float JumpSpeed;
    private bool readyForDoubleJump;
    [Space]
    public float StrikeTime;
    public float StrikePrepareVelocity;
    public float StrikeAttackVelocity;
    [Space]
    public float FlyKickPrepareVelocity;
    public float FlyKickAttackVelocity;
    public float DashStateVelocity = 20f;

    [Space]
    // Keycode
    public KeyCode JumpKeyCode,
                   CrouchKeyCode,
                   AttackKeyCode,
                   StrikeKeyCode,
                   FlyKickKeyCode,
                   DashKeyCode = KeyCode.LeftShift;

    [Space]
    // For trailing
    [SerializeField] TrailRenderer trailRenderer;
    public bool IsTrailing;


    // Use for animation
    float horizontalAxis;

    // About Flip
    public bool isFacingRight = true;

    [Space]
    [SerializeField] AudioSource audioSource;
    public AudioClip jumpAudio;
    public float JumpVolume = 1;

    private void OnValidate()
    {
        if (rb2D == null)
            rb2D = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
        if (rb2D == null)
            rb2D = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponent<Animator>();
        if (audioSource == null)
            audioSource = Camera.main.GetComponent<AudioSource>();


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

        // Set Animation Parameters
        animator.SetBool("Crouching", Input.GetKey(CrouchKeyCode));
        animator.SetBool("IsGrounded", checkGround.IsGrounded);
        animator.SetBool("Walking", horizontalAxis != 0);
        animator.SetBool("Attack", Input.GetKey(AttackKeyCode));

        // Check flip & Dash, Strike, FlyKick
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player Strike") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Fly Kick") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Die") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Dash"))
        {
            CheckFlip();

            // ALway true in this States;
            animator.SetBool("Strike", Input.GetKeyDown(StrikeKeyCode));
            animator.SetBool("FlyKick", Input.GetKeyDown(FlyKickKeyCode));
            animator.SetBool("Dash", Input.GetKeyDown(DashKeyCode));
        }


        CheckTrailing();

        CheckJump();

        // Movement on simple player state
        // Idle
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Idle"))
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Attack"))
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        // Walk
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Walk"))
            rb2D.velocity = new Vector2(horizontalAxis * WalkSpeed, rb2D.velocity.y);
        // Jump
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump"))
            rb2D.velocity = new Vector2(horizontalAxis * JumpSpeed, rb2D.velocity.y);
        // Jump Attack
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump Attack"))
            rb2D.velocity = new Vector2(horizontalAxis * JumpSpeed, rb2D.velocity.y);
        // Crouch
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Crouch"))
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Crouch Attack"))
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        // Strike & Fly Kick is so complex, so I put it in Behavior
    }

    private void CheckTrailing()
    {
        // Trailing
        if (IsTrailing)
        {
            trailRenderer.time = 0.25f;
        }
        else
        {
            if (trailRenderer.time > 0)
                trailRenderer.time -= Time.deltaTime;
        }
    }

    private void CheckJump()
    {
        if (checkGround.IsGrounded)
        {
            readyForDoubleJump = true;
            if (Input.GetKeyDown(JumpKeyCode))
            {
                rb2D.velocity = new Vector2(0, JumpVelocity);
                audioSource.PlayOneShot(jumpAudio, JumpVolume);
                //InstantiateDashWind();
            }
        }
        else if (readyForDoubleJump)
        {
            if (Input.GetKeyDown(JumpKeyCode))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump Attack"))
                    animator.Play("Player Jump", 0, 0f);
                audioSource.PlayOneShot(jumpAudio, JumpVolume);
                readyForDoubleJump = false;
                rb2D.velocity = new Vector2(0, JumpVelocity);
                //InstantiateDashWind();
            }
        }
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