using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackBehabvior : StateMachineBehaviour
{
    GoblinMovement goblinMovement;
    Rigidbody2D rb2D;
    EnemyZone enemyZone;

    float animationTime;
    float counter;
    bool shooted;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (goblinMovement == null) goblinMovement = animator.GetComponentInParent<GoblinMovement>();
        if (rb2D == null) rb2D = animator.GetComponentInParent<Rigidbody2D>();
        if (enemyZone == null) enemyZone = animator.transform.parent.transform.GetChild(3).GetComponent<EnemyZone>();

        rb2D.velocity = new Vector2(0, rb2D.velocity.y);

        animationTime = stateInfo.length;
        counter = 0;
        shooted = false;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!enemyZone.PlayerInZone)
            animator.SetBool("Attacking", false);

        counter += Time.deltaTime;
        if (!shooted && counter > animationTime / 3f)
        {
            shooted = true;

            if (goblinMovement.FacingRight)
                Instantiate(goblinMovement.ProjectTile, goblinMovement.ShootPoint.position, Quaternion.Euler(0, 0, 0));
            else
                Instantiate(goblinMovement.ProjectTile, goblinMovement.ShootPoint.position, Quaternion.Euler(0, 0, 180));
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
           
    }
}
