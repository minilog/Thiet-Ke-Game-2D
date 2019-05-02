using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashBehavior : StateMachineBehaviour
{
    PlayerController playerController;
    Rigidbody2D rb2D;
    public float DashVelocity = 25;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController == null)
            playerController = animator.gameObject.GetComponent<PlayerController>();
        if (rb2D == null)
            rb2D = animator.gameObject.GetComponent<Rigidbody2D>();

        playerController.IsTrailing = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (playerController.isFacingRight)
            rb2D.velocity = new Vector2(DashVelocity, 0);
        else
            rb2D.velocity = new Vector2(-DashVelocity, 0);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.IsTrailing = false;
    }
}
