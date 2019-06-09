using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWalkBehabvior : StateMachineBehaviour
{
    GoblinMovement goblinMovement;
    Rigidbody2D rb2D;
    EnemyZone enemyZone;

    bool AttackActive = false;  
    float count;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (goblinMovement == null) goblinMovement = animator.GetComponentInParent<GoblinMovement>();
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();
        if (enemyZone == null) enemyZone = animator.transform.parent.transform.GetChild(3).GetComponent<EnemyZone>();

        if (goblinMovement.FacingRight)
        {
            rb2D.velocity = new Vector2(goblinMovement.WalkSpeed, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(-goblinMovement.WalkSpeed, rb2D.velocity.y);
        }

        AttackActive = false;
        count = goblinMovement.AttackDelay;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (goblinMovement.FacingRight && goblinMovement.transform.position.x > goblinMovement.RightLimit.position.x)
        {
            goblinMovement.Flip();
            rb2D.velocity = new Vector2(-goblinMovement.WalkSpeed, rb2D.velocity.y);
        }
        else if (!goblinMovement.FacingRight && goblinMovement.transform.position.x < goblinMovement.LeftLimit.position.x)
        {
            goblinMovement.Flip();
            rb2D.velocity = new Vector2(goblinMovement.WalkSpeed, rb2D.velocity.y);
        }

        if (enemyZone.PlayerInZone)
        {
            AttackActive = true;
        }

        if (AttackActive)
        {
            count -= Time.deltaTime;
            if (count < 0)
                animator.SetBool("Attacking", true);

          
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
