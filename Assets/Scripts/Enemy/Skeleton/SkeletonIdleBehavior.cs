using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleBehavior : StateMachineBehaviour
{
    SkeletonMovement skeletonMovement;
    Rigidbody2D rb2D;

    float counter = 0.5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (skeletonMovement == null) skeletonMovement = animator.GetComponentInParent<SkeletonMovement>();
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();

        rb2D.velocity = new Vector2(0, rb2D.velocity.y);

        counter = stateInfo.length / 2f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (skeletonMovement.EnemyChasingZone.PlayerInZone)
        {
            animator.SetBool("ReadyChasing", true);
            counter = stateInfo.length / 2f;
        }

        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            animator.SetBool("ReadyChasing", false);
        }
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
