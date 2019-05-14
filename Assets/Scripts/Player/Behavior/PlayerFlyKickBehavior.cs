using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyKickBehavior : StateMachineBehaviour
{
    float prepareTime;
    [SerializeField] PlayerController playerController;
    [SerializeField] Rigidbody2D rb2D;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController == null)
            playerController = animator.gameObject.GetComponent<PlayerController>();
        if (rb2D == null)
            rb2D = animator.gameObject.GetComponent<Rigidbody2D>();

        prepareTime = stateInfo.length * 2f / 6f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prepareTime -= Time.deltaTime;

        if (prepareTime >= 0)
            rb2D.velocity = new Vector2(0, playerController.FlyKickPrepareVelocity);
        else
        {
            if (playerController.isFacingRight)
                rb2D.velocity = new Vector2(playerController.FlyKickAttackVelocity, 0);
            else
                rb2D.velocity = new Vector2(-playerController.FlyKickAttackVelocity, 0);
        }
    }
}
