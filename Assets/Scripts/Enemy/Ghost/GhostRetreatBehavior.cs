using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRetreatBehavior : StateMachineBehaviour
{
    GhostMovement ghostMovement;
    Rigidbody2D rb2D;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ghostMovement == null) ghostMovement = animator.GetComponentInParent<GhostMovement>();
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();


        ghostMovement.FlipTo(ghostMovement.Destination);

        Vector3 vec = ghostMovement.Destination.position - animator.transform.parent.transform.position;
        rb2D.velocity = (Vector2)vec.normalized * ghostMovement.ChaseSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        if (ghostMovement.IntoTrigger)
        {
            ghostMovement.Destination.position = ghostMovement.FirstPosition;
            animator.Play("Ghost Fly");
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
