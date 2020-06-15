using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AbilityManager : MonoBehaviour
{
    [SerializeField]
    float dashDistance=0;
    [SerializeField]
    float dashTime=0;


    float timer;


    [SerializeField]
    float distanceBehind;



    Vector3 dashDirection;


    Rigidbody rb;




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void FixedUpdate()
    {
        if (timer < dashTime)
        {
            rb.AddForce(dashDirection*dashDistance,ForceMode.Force);
            timer += Time.fixedDeltaTime;
        }
    }
   
    public void DashLeft() {
        Dash(-transform.right);
    }
    public void DashRight() {
        Dash(transform.right);
    }
    public void DashBack() {
        Dash(-transform.forward);
    
    }

    public void DashBehind(Transform target)
    {
        if (target == null) return;


        //Debug.Log(target.forward);


        transform.position = target.position;
        transform.position += -target.forward * distanceBehind;


        Vector3 relativePosition = target.position - transform.position;
        Quaternion rotationTowards=Quaternion.LookRotation(relativePosition);

        transform.rotation = Quaternion.Euler(0, rotationTowards.eulerAngles.y, 0);
        //transform.LookAt(target);
    }

    void Dash(Vector3 direction)
    {
        timer = 0;
        dashDirection = direction;
    }



}
