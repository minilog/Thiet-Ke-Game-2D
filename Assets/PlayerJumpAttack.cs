﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAttack : StateMachineBehaviour
{
    GameObject IblastPrefab;
    public Transform ShootPoint;
    float timeBtwShots;
    float StartTimeBtwShots;
    public PlayerController playerController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ShootPoint == null)
        {
            ShootPoint = animator.transform.Find("ShootPoint");
        }
        if (playerController == null)
        {
            playerController = animator.gameObject.GetComponent<PlayerController>();
        }
        if (IblastPrefab == null)
        {
            IblastPrefab = playerController.FireBallPrefab;
        }

        StartTimeBtwShots = stateInfo.length;
        timeBtwShots = StartTimeBtwShots / 1.45f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeBtwShots -= Time.deltaTime;
        if (timeBtwShots <= 0)
        {
            Vector3 angle;
            if (animator.transform.localScale.x == 1)
                angle = new Vector3(animator.transform.rotation.x, animator.transform.rotation.y, animator.transform.rotation.z - 90);
            else
                angle = new Vector3(animator.transform.rotation.x, animator.transform.rotation.y, animator.transform.rotation.z + 90);
            Instantiate(IblastPrefab, ShootPoint.position, Quaternion.Euler(angle));
            timeBtwShots += StartTimeBtwShots;
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
