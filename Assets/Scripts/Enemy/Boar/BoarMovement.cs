using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarMovement : MonoBehaviour
{
    public float ChaseSpeed;

    // facing
    public bool IsFacingRight = false;
    float autoFlipTime = 5f;
    float autoFlipCounter = 0f;

    // attacking
    public float WaitingToChaseTime;
    public float ChaseTime = 1.4f;


    public void CheckFlipAuto()
    {
        autoFlipCounter -= Time.deltaTime;

        if (autoFlipCounter < 0)
        {
            autoFlipCounter = autoFlipTime;

            if (Random.Range(0, 10) >= 5)
                Flip();
        }
    }

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        IsFacingRight = !IsFacingRight;
    }

    public bool IsPlayerOnTheRightSide()
    {
        return ObjectsInGame.PlayerController.transform.position.x > transform.position.x;
    }

    public void FacingToPlayer()
    {
        if (IsFacingRight && !IsPlayerOnTheRightSide())
            Flip();
        else if (!IsFacingRight && IsPlayerOnTheRightSide())
            Flip();

        autoFlipCounter = autoFlipTime;
    }
}
