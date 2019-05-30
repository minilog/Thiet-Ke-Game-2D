using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieBehavior : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Die", true);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        ObjectsInGame.PlayerController.Rb2D.velocity = new Vector2(0f, 0f);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ObjectsInGame.PlayerHealth.PlayerDieAndRestartLevel();
    }
}
