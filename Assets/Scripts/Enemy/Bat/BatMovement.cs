using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public Transform LeftLimit;
    public Transform RightLimit;
    public EnemyZone EnemyZone;
    public EnemyZone AttackZone;
    [Space]
    public bool FacingRight = true;
    public float FlySpeed = 3f;
    public float ChaseSpeed = 3f;
    public float ChaseTime = 5f;
    public float AttackSpeed = 10f;
    public Transform retreatDestination;
    public bool IntoTrigger = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyTrigger"))
        {
            IntoTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyTrigger"))
        {
            IntoTrigger = false;
        }
    }
}
