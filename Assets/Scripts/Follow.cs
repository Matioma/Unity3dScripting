using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    Transform target;

    [SerializeField]
    float sensitivity;



    Vector3 initialRotation;
    Vector3 initialPosition;

    
    void Start()
    {
        target = transform.parent;

        initialRotation =transform.localEulerAngles;
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            float mouseX = Input.GetAxis("Mouse X");

            transform.RotateAround(target.position, Vector3.up, mouseX * sensitivity * Time.deltaTime);
        }
        else {
            float scallingFactor =1;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(initialRotation), Time.deltaTime / scallingFactor);

            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime/scallingFactor);

            //transform.localRotation=Quaternion.Euler(initialRotation);
            //transform.localPosition = initialPosition;
        }
       
    }
}
