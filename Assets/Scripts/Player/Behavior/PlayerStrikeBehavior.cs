using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrikeBehavior : StateMachineBehaviour
{
    float strikeTimeCount;
    float prepareTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prepareTime = stateInfo.length * 2f / 5f;
        strikeTimeCount = ObjectsInGame.PlayerController.StrikeTime;
        animator.SetFloat("StrikeTime", ObjectsInGame.PlayerController.StrikeTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prepareTime -= Time.deltaTime;

        if (prepareTime >= 0)
            ObjectsInGame.PlayerController.Rb2D.velocity = new Vector2(0, ObjectsInGame.PlayerController.StrikePrepareYVelocity);
        else
        {
            if (ObjectsInGame.PlayerController.IsFacingRight)
                ObjectsInGame.PlayerController.Rb2D.velocity = new Vector2(ObjectsInGame.PlayerController.StrikeAttackXVelocity, 0);
            else
                ObjectsInGame.PlayerController.Rb2D.velocity = new Vector2(-ObjectsInGame.PlayerController.StrikeAttackXVelocity, 0);

            strikeTimeCount -= Time.deltaTime;
            animator.SetFloat("StrikeTime", strikeTimeCount);

            ObjectsInGame.PlayerController.IsTrailing = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ObjectsInGame.PlayerController.IsTrailing = false;
    }

}
