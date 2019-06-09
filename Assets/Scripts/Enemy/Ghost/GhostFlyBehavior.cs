using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFlyBehavior : StateMachineBehaviour
{
    GhostMovement ghostMovement;
    Rigidbody2D rb2D;
    EnemyZone enemyZone;

    float autoFlipCounter;

    bool readyToChase;
    float waitingToChaseCounter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();
        if (ghostMovement == null) ghostMovement = animator.GetComponentInParent<GhostMovement>();
        if (enemyZone == null) enemyZone = animator.transform.parent.GetComponentInChildren<EnemyZone>();

        autoFlipCounter = ghostMovement.AutoClipTime;
        rb2D.velocity = Vector2.zero;

        readyToChase = false;
        waitingToChaseCounter = ghostMovement.WaitingToChaseTime;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        autoFlipCounter -= Time.deltaTime;
        if (autoFlipCounter <= 0)
        {
            autoFlipCounter = ghostMovement.AutoClipTime;
            if (Random.Range(0, 100) < ghostMovement.AutoFlipRate)
                ghostMovement.Flip();
        }

        if (enemyZone.PlayerInZone)
        {
            readyToChase = true;
            ghostMovement.FlipTo(ObjectsInGame.PlayerController.transform);
        }

        if (readyToChase)
        {
            waitingToChaseCounter -= Time.deltaTime;
            if (waitingToChaseCounter <= 0)
            {
                ghostMovement.Destination.position = ObjectsInGame.PlayerController.transform.position;
                animator.Play("Ghost Chase");
            }
        }
    }

    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    //OnStateMove is called right after Animator.OnAnimatorMove()
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
