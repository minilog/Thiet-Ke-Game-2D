using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunBehavior : StateMachineBehaviour
{
    BossMovement bossMovement;
    Rigidbody2D rb2D;

    bool prepareAttack;
    float waitingToAttackCount;

    bool prepareIdle;
    float runToIdleCount;

    bool prepareJump;
    float runToJumpCount;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossMovement == null) bossMovement = animator.GetComponentInParent<BossMovement>();
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();

        if (bossMovement.FacingRight)
            rb2D.velocity = new Vector2(bossMovement.RunSpeed, rb2D.velocity.y);
        else
            rb2D.velocity = new Vector2(-bossMovement.RunSpeed, rb2D.velocity.y);

        //prepareAttack = false;
        //waitingToAttackCount = bossMovement.WaitingToAttackTime;

        prepareIdle = false;
        runToIdleCount = Random.Range(bossMovement.MinRunToIdleTime, bossMovement.MaxRunToIdleTime);

        prepareJump = false;
        runToJumpCount = bossMovement.RunToJumpTime;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // Check Running
        if (!bossMovement.ChaseZone.PlayerInZone)
            prepareIdle = true;

        if (prepareIdle && prepareJump == false)
        {
            runToIdleCount -= Time.deltaTime;
            if (runToIdleCount <= 0)
                animator.SetBool("Running", false);
        }

        // Check Attack
        if (bossMovement.AttackZone.PlayerInZone)
        {
            animator.SetTrigger("Attack");
            animator.SetBool("Running", false);
        }
        
        // Check Jump
        if (bossMovement.FacingRight && !bossMovement.IsPlayerOnTheRightSide() && bossMovement.DangerMode ||
            !bossMovement.FacingRight && bossMovement.IsPlayerOnTheRightSide() && bossMovement.DangerMode)
        {
            animator.SetTrigger("Jump");
            //prepareJump = true;
        }

        //if (prepareJump)
        //{
        //    runToJumpCount -= Time.deltaTime;
        //    if (runToJumpCount <= 0)
        //    {
        //        animator.SetTrigger("Jump");
        //    }
        //}

        //if (bossMovement.AttackZone.PlayerInZone)
        //    prepareAttack = true;

        //if (prepareAttack)
        //{
        //    waitingToAttackCount -= Time.deltaTime;
        //    if (waitingToAttackCount <= 0)
        //    {
        //        animator.SetTrigger("Attack");
        //        animator.SetBool("Running", false);
        //    }
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
