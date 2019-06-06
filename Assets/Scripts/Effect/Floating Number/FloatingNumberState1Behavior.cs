using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingNumberState1Behavior : StateMachineBehaviour
{
    FloatingNumber floatingNumber;
    Rigidbody2D rb2D;

    float xVelocity;
    float yVelocity;
    Vector3 positionOffset;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (floatingNumber == null) floatingNumber = animator.GetComponent<FloatingNumber>();
        rb2D = floatingNumber.rb2D;
        
        // Offset
        positionOffset = new Vector3(0, Random.Range(floatingNumber.MinYOffset_State1, floatingNumber.MaxYOffset_State1), 0);
        animator.transform.position += positionOffset;

        xVelocity = Random.Range(floatingNumber.MinXVelocity_State1, floatingNumber.MaxXVelocity_State1);
        if (!floatingNumber.RightDirection)
            xVelocity = -xVelocity;

        yVelocity = Random.Range(floatingNumber.MinYVelocity_State1, floatingNumber.MaxYVelocity_State1);

        rb2D.velocity = new Vector2(xVelocity, yVelocity);
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
