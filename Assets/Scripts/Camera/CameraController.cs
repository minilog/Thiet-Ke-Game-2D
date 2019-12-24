using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // FOLLOW PLAYER
    [SerializeField] GameObject followTarget;

    // BOUNDS
    private BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    Rigidbody2D rb2D;
    [Space]
    public float SpeedFollowX = 2f;
    public float SpeedFollowY = 2f;
    public float LerpSpeed = 15;

    private void Start()
    {
        FindBounds();

        // The camera
        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
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

    private void LateUpdate()
    {
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

        Vector3 v = targetPos - transform.position;
        rb2D.velocity = new Vector2(v.x * SpeedFollowX, v.y * SpeedFollowY);
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
