using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashBehavior : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ObjectsInGame.PlayerController.IsTrailing = true;


    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (ObjectsInGame.PlayerController.IsFacingRight)
            ObjectsInGame.PlayerController.Rb2D.velocity = new Vector2(ObjectsInGame.PlayerController.DashXVelocity, 0);
        else
            ObjectsInGame.PlayerController.Rb2D.velocity = new Vector2(-ObjectsInGame.PlayerController.DashXVelocity, 0);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ObjectsInGame.PlayerController.IsTrailing = false;
    }
}
