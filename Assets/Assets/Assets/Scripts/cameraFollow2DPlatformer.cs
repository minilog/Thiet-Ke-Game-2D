using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow2DPlatformer : MonoBehaviour
{
    public Transform target; // what camera is following
    public float smoothing; // dempening effect

    Vector3 offset;

    float lowY;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;

        lowY = transform.position.y;
    }

    void FixedUpdate()
    {
        Vector3 targetCampos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCampos, smoothing * Time.deltaTime);

        if (transform.position.y < lowY)
        {
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        }
    }
}
