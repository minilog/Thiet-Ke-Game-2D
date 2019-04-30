using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //public float Smooth;
    [SerializeField] Transform camera;
    Vector3 offset;
    private void Start()
    {
        offset = camera.position - transform.position;
    }

    private void LateUpdate()
    {
        //camera.position += (transform.position + offset - camera.transform.position) * Time.unscaledDeltaTime * Smooth;
        camera.position = transform.position + offset;
    }
}
