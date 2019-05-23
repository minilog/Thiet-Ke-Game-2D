using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrounchBehavior : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ObjectsInGame.PlayerController.EnableCrouchBoxCollider();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ObjectsInGame.PlayerController.EnableNormalBoxCollider();
    }
}
