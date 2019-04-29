﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyKickBehavior : StateMachineBehaviour
{
    float prepareTime;
    [SerializeField] PlayerController playerController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController == null)
            playerController = animator.gameObject.GetComponent<PlayerController>();

        prepareTime = stateInfo.length * 2f / 6f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prepareTime -= Time.deltaTime;

        if (prepareTime >= 0)
            animator.transform.Translate(new Vector3(0, playerController.FlyKichPrepare * Time.deltaTime, 0));
        else
        {
            if (playerController.isFacingRight)
                animator.transform.Translate(new Vector3(playerController.FlyKichAttack * Time.deltaTime, 0, 0));
            else
                 animator.transform.Translate(new Vector3(-playerController.FlyKichAttack * Time.deltaTime, 0, 0));
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
