using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Movement & Animation
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rb2D;
    [SerializeField] Animator animator;
    [SerializeField] CheckGround checkGround;
    [SerializeField] BoxCollider2D normalBoxCollider;
    [SerializeField] BoxCollider2D crouchBoxCollider;
    [SerializeField] TrailRenderer trailRenderer;

    [Space]
    public float WalkXVelocity;
    public float JumpXVelocity;
    public float JumpYVelocity;

    private bool readyForDoubleJump;
    public float StrikeTime;
    public float StrikePrepareYVelocity;
    public float StrikeAttackXVelocity;
    public float FlyKickYPrepareVelocity;
    public float FlyKickAttackXVelocity;
    public float DashXVelocity = 20f;

    public float MaxFallSpeed = 20f;

    [Space]
    // Keycode
    public KeyCode JumpKeyCode,
                   CrouchKeyCode,
                   AttackKeyCode,
                   StrikeKeyCode,
                   FlyKickKeyCode,
                   DashKeyCode = KeyCode.LeftShift;

    public bool IsTrailing { get; set; } = false;

    // Use for animation
    float horizontalAxis;

    // About Flip
    public bool IsFacingRight { get; private set; } = false;

    [Space]
    public float DashCooldown = 2.5f;
    public float DashCooldownCount = 0f;

    PlayerStamina playerStamina;

    [Space]
    [SerializeField] GameObject cameraTarget;
    public float MaxTargetX = 4;
    public float TargetMoveAddition = 2;
    public float TargetMoveAddition2 = 1;
    public float TargetMoveSpeed = 2f;

    public bool DoubleJumpAtive = false;

    [SerializeField] Joystick joy;
    public static bool IsAttack = false,
        isJump = false,
        isDash = false;

    private void Awake()    
    {
        // To reuse in game
        ObjectsInGame.PlayerController = this;
    }

    private void Start()
    {
        // Slower animation
        animator.speed = 0.66f;
        playerStamina = GetComponent<PlayerStamina>();
    }

    private void Update()
    {
        // controll camera
        Vector3 targetPos = cameraTarget.transform.localPosition;
        targetPos.x += Mathf.Abs(Rb2D.velocity.x) * (MaxTargetX -  targetPos.x) * TargetMoveSpeed * Time.deltaTime;
        if (targetPos.x > MaxTargetX)
            targetPos.x = MaxTargetX;
        cameraTarget.transform.localPosition = targetPos;

        if (Rb2D.velocity.y <= -MaxFallSpeed)
        {
            Rb2D.velocity = new Vector2(Rb2D.velocity.x, -MaxFallSpeed);
        }

        horizontalAxis = joy.Horizontal;
        if (horizontalAxis < 0)
        {
            horizontalAxis = -1;
        }
        else if (horizontalAxis > 0)
        {
            horizontalAxis = 1;
        }

        // Set Animation Parameters
        animator.SetBool("Crouching", Input.GetKey(CrouchKeyCode));

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump") && Rb2D.velocity.y > 0)
        {
            animator.SetBool("IsGrounded", false);
        }
        else
        {
            animator.SetBool("IsGrounded", checkGround.IsGrounded);
        }

        animator.SetBool("Walking", horizontalAxis != 0);

        if (playerStamina.IsEnoughStaminaForFire())
            animator.SetBool("Attack", IsAttack);
        else
            animator.SetBool("Attack", false);

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

            if (isDash && playerStamina.CheckDashStamina())
            {
                animator.SetBool("Dash", true);
            }
            else
            {
                animator.SetBool("Dash", false);
            }
        }


        CheckTrailing();

        CheckJump();

        // Movement on simple player state
        // Idle
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Idle"))
            Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Attack"))
            Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
        // Walk
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Walk"))
            Rb2D.velocity = new Vector2(horizontalAxis * WalkXVelocity, Rb2D.velocity.y);
        // Jump
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump"))
        {
            Rb2D.velocity = new Vector2(horizontalAxis * JumpXVelocity, Rb2D.velocity.y);
        }
        // Jump Attack
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump Attack"))
            Rb2D.velocity = new Vector2(horizontalAxis * JumpXVelocity, Rb2D.velocity.y);
        // Crouch
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Crouch"))
            Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Crouch Attack"))
            Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
        // Strike & Fly Kick is so complex, so I put it in Behavior

        isDash = false;
        isJump = false;
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
        if (!DoubleJumpAtive)
            readyForDoubleJump = false;

        if (checkGround.IsGrounded)
        {
            readyForDoubleJump = true;
            if (isJump)
            {
                Rb2D.velocity = new Vector2(0, JumpYVelocity);
                ObjectsInGame.SoundManager.PlayPlayerJumpAudioClip();
            }
        }
        else if (readyForDoubleJump)
        {
            if (isJump)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump Attack"))
                    animator.Play("Player Jump", 0, 0f);
                ObjectsInGame.SoundManager.PlayPlayerJumpAudioClip();
                readyForDoubleJump = false;
                Rb2D.velocity = new Vector2(0, JumpYVelocity);
            }
        }
    }

    private void CheckFlip()
    {
        if (horizontalAxis < 0 && IsFacingRight)
        {
            IsFacingRight = false;
            Flip();
        }
        else if (horizontalAxis > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
            Flip();
        }
    } 

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;

        // TARGET
        Vector3 targetPos = cameraTarget.transform.localPosition;
        targetPos.x = -targetPos.x;
        if (targetPos.x < 0)
            targetPos.x += TargetMoveAddition;
        else
            targetPos.x += TargetMoveAddition2;
        cameraTarget.transform.localPosition = targetPos;
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

    public void Button_AttackDown()
    {
        IsAttack = true;
    }
    public void Button_AttackUp()
    {
        IsAttack = false;
    }
    public void Button_JumpDown()
    {
        isJump = true;
    }
    public void Button_JumpUp()
    {
        isJump = false;
    }
    public void Button_DashDown()
    {
        isDash = true;
    }
    public void Button_DashUp()
    {
        isDash = false;
    }
}