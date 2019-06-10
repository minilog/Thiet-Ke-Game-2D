using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBehavior : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ObjectsInGame.PlayerController.Rb2D.velocity = ObjectsInGame.PlayerHealth.HurtDirection;

        ObjectsInGame.SoundManager.PlayHitCLip();
    }
}
