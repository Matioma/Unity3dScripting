using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AbilityManager : MonoBehaviour
{
    [SerializeField]
    float dashDistance;
    [SerializeField]
    float dashTime;


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


        transform.position = -target.transform.forward * distanceBehind;
        //transform.LookAt(target);
    }

    void Dash(Vector3 direction)
    {
        timer = 0;
        dashDirection = direction;
    }



}
