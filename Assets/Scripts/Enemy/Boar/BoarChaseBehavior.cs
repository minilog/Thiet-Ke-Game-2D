using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseBehavior : StateMachineBehaviour
{
    private BoarMovement boarMovement;
    private Rigidbody2D rb2D;
    private EnemyZone boarsChaseZone;

    private bool outOfRange = false;
    private float chaseCounter;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boarMovement == null)
            boarMovement = animator.GetComponentInParent<BoarMovement>();
        if (rb2D == null)
            rb2D = animator.GetComponentInParent<Rigidbody2D>();
        if (boarsChaseZone == null)
            boarsChaseZone = animator.transform.parent.GetChild(2).GetComponent<EnemyZone>();
        if (boarMovement.IsPlayerOnTheRightSide())
            rb2D.velocity = new Vector2(boarMovement.ChaseSpeed, rb2D.velocity.y);
        else
            rb2D.velocity = new Vector2(-boarMovement.ChaseSpeed, rb2D.velocity.y);

        ////////////
        outOfRange = false;
        chaseCounter = boarMovement.ChaseTime;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!boarsChaseZone.PlayerInZone)
            outOfRange = true;

        if (boarMovement.IsFacingRight && !boarMovement.IsPlayerOnTheRightSide())
            outOfRange = true;
        else if (!boarMovement.IsFacingRight && boarMovement.IsPlayerOnTheRightSide())
            outOfRange = true;

        if (outOfRange)
        {
            chaseCounter -= Time.deltaTime;
            if (chaseCounter <= 0)
                animator.SetBool("IsChasing", false);
        }
    }
}
