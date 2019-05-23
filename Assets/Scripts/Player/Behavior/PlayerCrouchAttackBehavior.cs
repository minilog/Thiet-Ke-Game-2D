using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchAttackBehavior : StateMachineBehaviour
{
    float timeBtwShots;
    float StartTimeBtwShots;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StartTimeBtwShots = stateInfo.length;
        timeBtwShots = StartTimeBtwShots / 1.45f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeBtwShots -= Time.deltaTime;
        if (timeBtwShots <= 0)
        {
            Vector3 angle;
            if (animator.transform.localScale.x > 0)
                angle = new Vector3(animator.transform.rotation.x, animator.transform.rotation.y, animator.transform.rotation.z - 90);
            else
                angle = new Vector3(animator.transform.rotation.x, animator.transform.rotation.y, animator.transform.rotation.z + 90);

            Instantiate(ObjectsInGame.PlayerDamage.Projectile, ObjectsInGame.PlayerDamage.CrouchShootPoint.position, Quaternion.Euler(angle));
            timeBtwShots += StartTimeBtwShots;
        }
    }
}
