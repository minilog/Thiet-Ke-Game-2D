using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStrikeBehavior : StateMachineBehaviour
{
    BossMovement bossMovement;
    Rigidbody2D rb2D;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossMovement == null) bossMovement = animator.GetComponentInParent<BossMovement>();
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();

        if (bossMovement.FacingRight)
            rb2D.velocity = new Vector2(bossMovement.StrikeSpeed, rb2D.velocity.y);
        else
            rb2D.velocity = new Vector2(-bossMovement.StrikeSpeed, rb2D.velocity.y);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
