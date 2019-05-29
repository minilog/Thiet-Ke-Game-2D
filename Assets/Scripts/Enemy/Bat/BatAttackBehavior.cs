using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackBehavior : StateMachineBehaviour
{
    BatMovement batMovement;
    Rigidbody2D rb2D;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (batMovement == null) batMovement = animator.GetComponentInParent<BatMovement>();
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();

        Vector3 vec = ObjectsInGame.PlayerController.transform.position - animator.transform.position;

        rb2D.velocity = (Vector2)vec.normalized * batMovement.AttackSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
