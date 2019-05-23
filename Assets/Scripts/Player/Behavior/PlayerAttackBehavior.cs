using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehavior : StateMachineBehaviour
{
    float timeBtwShots;
    float StartTimeBtwShots;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Time from Animation
        StartTimeBtwShots = stateInfo.length;
        timeBtwShots = StartTimeBtwShots / 1.45f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // We need reduce time between shot every frame
        timeBtwShots -= Time.deltaTime;
        if (timeBtwShots <= 0)
        {
            Vector3 angle;
            if (animator.transform.localScale.x == 1)
                angle = new Vector3(animator.transform.rotation.x, animator.transform.rotation.y, animator.transform.rotation.z - 90);
            else
                angle = new Vector3(animator.transform.rotation.x, animator.transform.rotation.y, animator.transform.rotation.z + 90);

            Instantiate(ObjectsInGame.PlayerDamage.Projectile, ObjectsInGame.PlayerDamage.ShootPoint.position, Quaternion.Euler(angle));
            timeBtwShots += StartTimeBtwShots;
        }
    }
}
