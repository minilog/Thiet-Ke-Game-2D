using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingNumberState2Behavior : StateMachineBehaviour
{
    FloatingNumber floatingNumber;
    Rigidbody2D rb2D;

    bool triggerGravity;
    bool setGravity;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (floatingNumber == null) floatingNumber = animator.GetComponent<FloatingNumber>();
        rb2D = floatingNumber.rb2D;

        triggerGravity = false;
        setGravity = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!triggerGravity)
        {
            rb2D.velocity = Vector2.Lerp(rb2D.velocity, Vector2.zero, floatingNumber.LerpSpeedVelocity_State2 * Time.deltaTime);


            if (rb2D.velocity == Vector2.zero)
            {
                triggerGravity = true;
                setGravity = true;
            }
        }

        if (setGravity)
        {
            float ran = Random.Range(0, floatingNumber.GraviryLessThan0Percent + floatingNumber.GraviryEqual0Percent + floatingNumber.GraviryGreaterThan0Percent);
            float gScale = Random.Range(floatingNumber.MinGravityScale_State2, floatingNumber.MaxGravityScale_State2);

            if (ran < floatingNumber.GraviryLessThan0Percent)
            {
                rb2D.gravityScale = -gScale;
            }
            else if (ran < floatingNumber.GraviryLessThan0Percent + floatingNumber.GraviryEqual0Percent)
            {
                rb2D.gravityScale = 0;
            }
            else
            {
                rb2D.gravityScale = gScale;
            }

            setGravity = false;
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
