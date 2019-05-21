using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // FOLLOW PLAYER
    [SerializeField] GameObject followTarget;
    [SerializeField] float moveSpeed;
    public float boundX = 2.0f;
    public float boundY = 1.5f;

    // BOUNDS
    private BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    private void Start()
    {
        FindBounds();

        // The camera
        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    private void LateUpdate()
    {
        FollowPlayer();

        FindBounds();

        // Keep camera inside
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        if (clampedY < minBounds.y + halfHeight)
            clampedY = minBounds.y + halfHeight;
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void FollowPlayer()
    {
        Vector3 targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);

        Vector3 delta = Vector2.zero;

        float dx = targetPos.x - transform.position.x;
        if (dx > boundX || dx < -boundX)
        {
            if (transform.position.x < targetPos.x)
            {
                delta.x = dx - boundX;
            }
            else
            {
                delta.x = dx + boundX;
            }
        }

        float dy = targetPos.y - transform.position.y;
        if (dy > boundY || dy < -boundY)
        {
            if (transform.position.y < targetPos.y)
            {
                delta.y = dy - boundY;
            }
            else
            {
                delta.y = dy + boundY;
            }
        }

        transform.position = transform.position + delta;
    }

    private void FindBounds()
    {
        if (boundBox == null)
        {
            // WARNING
            boundBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }
    }
}
