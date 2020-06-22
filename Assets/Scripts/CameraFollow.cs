using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    float sensitivity;
    [SerializeField]
    float returnCameraSpeed;


    Vector3 offset;

    Quaternion defaultRotation;

    
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;


        defaultRotation = transform.rotation;
        offset = (transform.position - target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            return;
        }

        if (Input.GetButton("Fire3"))
        {
            float mouseX = Input.GetAxis("Mouse X");

            Quaternion targetYRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y+ mouseX * sensitivity*Time.deltaTime, 0);

            transform.position = target.position + targetYRotation * offset;
            transform.rotation = targetYRotation*defaultRotation;
        }
        else {
            Quaternion targetYRotation = Quaternion.Euler(0, target.rotation.eulerAngles.y, 0);

            transform.position = target.position + targetYRotation * offset;
            transform.rotation = targetYRotation * defaultRotation;
        }
    }
}
