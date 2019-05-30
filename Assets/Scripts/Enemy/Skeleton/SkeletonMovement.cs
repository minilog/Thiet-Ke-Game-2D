using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public Transform LeftLimit;
    public Transform RightLimit;
    public EnemyZone EnemyZone;
    public EnemyZone EnemyChasingZone;
    [Space]
    public bool FacingRight = false;
    public float WalkSpeed = 3f;
    public float ChaseSpeed = 6f;
    public float ChasingTimeWhenNotSeePlayer = 0.6f;
    public float FlipToPlayerRate = 50f;


    public void Flip()
    {
        FacingRight = !FacingRight;

        // flip GoBlin
        Vector3 localScale = transform.localScale;
        localScale.x = -localScale.x;
        transform.localScale = localScale;

        // flip Health Canvas
        Vector3 childScale = gameObject.transform.GetChild(0).localScale;
        childScale.x = -childScale.x;
        gameObject.transform.GetChild(0).localScale = childScale;
    }

    public void FlipToPlayer()
    {
        if (FacingRight && transform.position.x > ObjectsInGame.PlayerController.transform.position.x)
        {
            Flip();
        }
        else if (!FacingRight && transform.position.x < ObjectsInGame.PlayerController.transform.position.x)
        {
            Flip();
        }
    }

    public void NotFacingToPlayer()
    {
        if (FacingRight && transform.position.x < ObjectsInGame.PlayerController.transform.position.x)
        {
            Flip();
        }
        else if (!FacingRight && transform.position.x > ObjectsInGame.PlayerController.transform.position.x)
        {
            Flip();
        }
    }

    public void FlipTo(Transform gObjectTransform)
    {
        if (FacingRight && transform.position.x > gObjectTransform.position.x)
        {
            Flip();
        }
        else if (!FacingRight && transform.position.x < gObjectTransform.position.x)
        {
            Flip();
        }
    }
}
