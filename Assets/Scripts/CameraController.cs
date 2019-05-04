using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    [SerializeField] Rigidbody2D rb2D;
    public float SmoothHorizontal;
    public float SmoothVertical;

    private void LateUpdate()
    {

        Vector2 delta = Target.position - transform.position;
        //if (Mathf.Abs(delta.x) > 1 || Mathf.Abs(delta.y) > 1)
            rb2D.velocity = new Vector2(delta.x * SmoothHorizontal, delta.y * SmoothVertical);

    }
}
