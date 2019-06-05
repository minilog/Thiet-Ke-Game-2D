using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetScript : MonoBehaviour
{
    [SerializeField] GameObject cameraTarget;
    public float MaxSize;
    Animator anim;

    float current;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetAxisRaw("Horizontal") != 0)
        //{
        //    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player Dash"))
        //    {
        //        current += Input.GetAxis("Horizontal");
        //        if (current > MaxSize)
        //            current = MaxSize;
        //        if (current < 0)
        //            current = 0;
        //    }
        //}

        //cameraTarget.transform.position = new Vector3(current, cameraTarget.transform.position.y, cameraTarget.transform.position.z);
    }
}
