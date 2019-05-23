using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyKickBehavior : StateMachineBehaviour
{
    float prepareTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prepareTime = stateInfo.length * 2f / 6f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prepareTime -= Time.deltaTime;

        if (prepareTime >= 0)
            ObjectsInGame.PlayerController.Rb2D.velocity = new Vector2(0, ObjectsInGame.PlayerController.FlyKickYPrepareVelocity);
        else
        {
            if (ObjectsInGame.PlayerController.IsFacingRight)
                ObjectsInGame.PlayerController.Rb2D.velocity = new Vector2(ObjectsInGame.PlayerController.FlyKickAttackXVelocity, 0);
            else
                ObjectsInGame.PlayerController.Rb2D.velocity = new Vector2(-ObjectsInGame.PlayerController.FlyKickAttackXVelocity, 0);
        }
    }
}
