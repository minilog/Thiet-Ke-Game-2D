using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatChaseBehavior : StateMachineBehaviour
{
    BatMovement batMovement;
    Rigidbody2D rb2D;

    float chaseCounter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (batMovement == null) batMovement = animator.transform.parent.GetComponent<BatMovement>();
        if (rb2D == null) rb2D = animator.transform.parent.GetComponent<Rigidbody2D>();

        chaseCounter = batMovement.ChaseTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        batMovement.FlipToPlayer();

        Vector3 vec = ObjectsInGame.PlayerController.transform.position - animator.transform.parent.transform.position;
        rb2D.velocity = (Vector2)vec.normalized * batMovement.ChaseSpeed;

        chaseCounter -= Time.deltaTime;
        if (chaseCounter <= 0)
            animator.SetTrigger("Retreat");


        if (batMovement.AttackZone.PlayerInZone)
        {
            animator.Play("Bat Attack");
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
