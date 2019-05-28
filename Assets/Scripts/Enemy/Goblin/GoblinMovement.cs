using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    public float WalkSpeed;
    public bool FacingRight = true;
    public Transform LeftLimit;
    public Transform RightLimit;
    public GameObject ProjectTile;
    public Transform ShootPoint;

    public float AttackDelay = 0.5f;


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
}
