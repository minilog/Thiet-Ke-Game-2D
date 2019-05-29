using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFlyBehavior : StateMachineBehaviour
{
    BatMovement batMovement;
    Rigidbody2D rb2D;
    EnemyZone enemyZone;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (batMovement == null) batMovement = animator.GetComponentInParent<BatMovement>();
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();

        if (batMovement.FacingRight)
        {
            rb2D.velocity = new Vector2(batMovement.FlySpeed, 0);
        }
        else
        {
            rb2D.velocity = new Vector2(-batMovement.FlySpeed, 0);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (batMovement.FacingRight && batMovement.transform.position.x > batMovement.RightLimit.position.x)
        {
            batMovement.Flip();
            rb2D.velocity = new Vector2(-batMovement.FlySpeed, 0);
        }
        else if (!batMovement.FacingRight && batMovement.transform.position.x < batMovement.LeftLimit.position.x)
        {
            batMovement.Flip();
            rb2D.velocity = new Vector2(batMovement.FlySpeed, 0);
        }

        if (batMovement.EnemyZone.PlayerInZone)
        {
            animator.SetTrigger("Chase");
        }

    }

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
