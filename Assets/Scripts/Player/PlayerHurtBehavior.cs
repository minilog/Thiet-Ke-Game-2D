using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBehavior : StateMachineBehaviour
{
    Rigidbody2D rb2D;
    PlayerInteraction playerInteraction;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (rb2D == null)
            rb2D = animator.gameObject.GetComponent<Rigidbody2D>();
        if (playerInteraction == null)
            playerInteraction = animator.gameObject.GetComponent<PlayerInteraction>();

        rb2D.velocity = new Vector2(playerInteraction.pushForceXWhenTakeDamage, 5f);
    }
}
