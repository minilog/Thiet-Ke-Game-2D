using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrikeBehavior : StateMachineBehaviour
{
    public PlayerController playerController;
    public Rigidbody2D rb2D;
    public float StrikeTime = 1f;
    float strikeTimeCount;
    private float prepareTime;
    public float PrepareSpeedGoingUp;
    public float StrikeSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController == null)
            playerController = animator.gameObject.GetComponent<PlayerController>();
        if (rb2D == null)
            rb2D = animator.gameObject.gameObject.GetComponent<Rigidbody2D>();

        prepareTime = stateInfo.length * 2f / 5f;
        strikeTimeCount = StrikeTime;
        animator.SetFloat("StrikeTime", strikeTimeCount);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        strikeTimeCount -= Time.deltaTime;
        prepareTime -= Time.deltaTime;
        if (prepareTime >= 0)
            rb2D.velocity = new Vector2(0f, PrepareSpeedGoingUp * Time.deltaTime);
        else
        {
            if (playerController.isFacingRight)
                rb2D.velocity = new Vector2(StrikeSpeed * Time.deltaTime, 0f);
            else
                rb2D.velocity = new Vector2(-StrikeSpeed * Time.deltaTime, 0f);
        }

        animator.SetFloat("StrikeTime", strikeTimeCount);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
