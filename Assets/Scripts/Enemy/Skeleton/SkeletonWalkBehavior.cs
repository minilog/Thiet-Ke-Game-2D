using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWalkBehavior : StateMachineBehaviour
{
    SkeletonMovement skeletonMovement;
    Rigidbody2D rb2D;

    float counter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (skeletonMovement == null) skeletonMovement = animator.GetComponentInParent<SkeletonMovement>();
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();

        counter = 2f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (skeletonMovement.FacingRight)
        {
            rb2D.velocity = new Vector2(skeletonMovement.WalkSpeed, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(-skeletonMovement.WalkSpeed, rb2D.velocity.y);
        }

        if (skeletonMovement.FacingRight && skeletonMovement.transform.position.x > skeletonMovement.RightLimit.position.x)
        {
            skeletonMovement.Flip();
        }
        else if (!skeletonMovement.FacingRight && skeletonMovement.transform.position.x < skeletonMovement.LeftLimit.position.x)
        {
            skeletonMovement.Flip();
        }

        if (skeletonMovement.EnemyZone.PlayerInZone)
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
