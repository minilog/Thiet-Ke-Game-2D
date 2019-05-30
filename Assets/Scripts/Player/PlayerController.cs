﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        // To reuse in game
        ObjectsInGame.PlayerController = this;
    }

    private void Start()
    {
        // Slower animation
        animator.speed = 0.66f;
    }

    private void Update()
    {
        // Time Scale
        //if (Input.GetKey(KeyCode.Q))
        //    Time.timeScale = 0.1f;
        //else
        //    Time.timeScale = 1f;


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
            Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Attack"))
            Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
        // Walk
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Walk"))
            Rb2D.velocity = new Vector2(horizontalAxis * WalkXVelocity, Rb2D.velocity.y);
        // Jump
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump"))
            Rb2D.velocity = new Vector2(horizontalAxis * JumpXVelocity, Rb2D.velocity.y);
        // Jump Attack
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump Attack"))
            Rb2D.velocity = new Vector2(horizontalAxis * JumpXVelocity, Rb2D.velocity.y);
        // Crouch
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Crouch"))
            Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Crouch Attack"))
            Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
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
                Rb2D.velocity = new Vector2(0, JumpYVelocity);
                ObjectsInGame.SoundManager.PlayPlayerJumpAudioClip();
            }
        }
        else if (readyForDoubleJump)
        {
            if (Input.GetKeyDown(JumpKeyCode))
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
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
        else if (horizontalAxis > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
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