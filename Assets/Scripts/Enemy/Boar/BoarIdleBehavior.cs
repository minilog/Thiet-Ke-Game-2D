using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarIdleBehavior : StateMachineBehaviour
{
    private BoarMovement boarMovement;
    private Rigidbody2D rb2D;
    private BoarsChaseZone boarsChaseZone;

    private float waitingToChaseCounter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boarMovement == null)
            boarMovement = animator.GetComponentInParent<BoarMovement>();
        if (rb2D == null)
            rb2D = animator.GetComponentInParent<Rigidbody2D>();
        if (boarsChaseZone == null)
            boarsChaseZone = animator.transform.parent.GetChild(2).GetComponent<BoarsChaseZone>();

        rb2D.velocity = new Vector2(0, rb2D.velocity.y);

        waitingToChaseCounter = boarMovement.WaitingToChaseTime;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boarsChaseZone.PlayerInChaseZone)
        {
            waitingToChaseCounter -= Time.deltaTime;

            boarMovement.FacingToPlayer();

            if (waitingToChaseCounter <= 0)
                animator.SetBool("IsChasing", true);
        }
        else
        {
            waitingToChaseCounter = boarMovement.WaitingToChaseTime;

            boarMovement.CheckFlipAuto();
        }
    }
}
