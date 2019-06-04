using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleBehavior : StateMachineBehaviour
{
    BossMovement bossMovement;
    Rigidbody2D rb2D;

    bool prepareAttack;
    float waitingToAttackCount;

    float maxIdleCount;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossMovement == null) bossMovement = animator.GetComponentInParent<BossMovement>();
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();

        rb2D.velocity = new Vector2(0, rb2D.velocity.y);

        prepareAttack = false;
        waitingToAttackCount = bossMovement.WaitingToAttackTime;

        maxIdleCount = Random.Range(bossMovement.MinIdleTime, bossMovement.MaxIdleTime);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossMovement.AttackZone.PlayerInZone)
            prepareAttack = true;

        if (prepareAttack)
        {
            waitingToAttackCount -= Time.deltaTime;
            if (waitingToAttackCount <= 0)
                animator.SetTrigger("Attack");
        }

        maxIdleCount -= Time.deltaTime;
        if (maxIdleCount <= 0)
        {
            if (Random.Range(0, 100) <= bossMovement.StrikeRate && bossMovement.DangerMode)
                animator.SetTrigger("Strike");
            else
                animator.SetBool("Running", true);
        }

        bossMovement.FlipToPlayer();

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
