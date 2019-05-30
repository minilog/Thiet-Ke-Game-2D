using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonChaseBehavior : StateMachineBehaviour
{
    SkeletonMovement skeletonMovement;
    Rigidbody2D rb2D;

    float counter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (skeletonMovement == null) skeletonMovement = animator.GetComponentInParent<SkeletonMovement>();
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();

        if (skeletonMovement.FacingRight)
            rb2D.velocity = new Vector2(skeletonMovement.ChaseSpeed, rb2D.velocity.y);
        else
            rb2D.velocity = new Vector2(-skeletonMovement.ChaseSpeed, rb2D.velocity.y);

        counter = skeletonMovement.ChasingTimeWhenNotSeePlayer;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (skeletonMovement.EnemyChasingZone.PlayerInZone)
        {
            counter = skeletonMovement.ChasingTimeWhenNotSeePlayer;
        }

        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            animator.Play("Skeleton Idle");
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
