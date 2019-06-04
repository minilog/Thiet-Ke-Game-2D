﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public EnemyZone AttackZone;
    public EnemyZone ChaseZone;
    [SerializeField] CheckGround checkGround;
    [SerializeField] Animator animator;
    [SerializeField] EnemyHealth enemyHealth;
    [Space]
    public float WaitingToAttackTime = 0.25f;
    public float MinIdleTime = 1;
    public float MaxIdleTime = 2;
    public float MinRunToIdleTime = 0.25f;
    public float MaxRunToIdleTime = 0.5f;
    public float RunToJumpTime = 0.2f;
    [Range(0, 100)]
    public float StrikeRate = 99;

    [Space]
    public float RunSpeed = 4f;
    public float JumpXSpeed = 6f;
    public float JumpYSpeed = 6f;
    public bool DangerMode = true;
    public float HPDangerMode = 50;
    public float StrikeSpeed = 10f;

    public bool FacingRight { get; private set; } = true;


    private void Start()
    {
        DangerMode = false;
    }

    private void Update()
    {
        animator.SetBool("IsGround", checkGround.IsGrounded);

        if (enemyHealth.Health <= HPDangerMode)
        {
            DangerMode = true;
        }

    }

    public void Flip()
    {
        FacingRight = !FacingRight;

        // flip GoBlin
        Vector3 localScale = transform.localScale;
        localScale.x = -localScale.x;
        transform.localScale = localScale;;
    }

    public void FlipToPlayer()
    {
        if (FacingRight && transform.position.x > ObjectsInGame.PlayerController.transform.position.x)
        {
            Flip();
        }
        else if (!FacingRight && transform.position.x < ObjectsInGame.PlayerController.transform.position.x)
        {
            Flip();
        }
    }

    public bool IsPlayerOnTheRightSide()
    {
        return transform.position.x < ObjectsInGame.PlayerController.transform.position.x;
    }
}