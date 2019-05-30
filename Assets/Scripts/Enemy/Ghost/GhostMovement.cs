using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Transform EnemyZone;
    public float AutoClipTime = 3;
    [Range(0, 100)]
    public float AutoFlipRate = 60;
    [Space]
    public bool FacingRight = true;
    public float ChaseSpeed = 5;
    public Transform Destination;
    public Vector3 FirstPosition { private set; get; }
    public bool IntoTrigger = false;
    public float WaitingToChaseTime = 0.8f;
    public float WaitingToRetreat = 0.4f;

    private void Start()
    {
        FirstPosition = Destination.position;
    }

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
