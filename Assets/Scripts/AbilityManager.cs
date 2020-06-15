using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AbilityManager : MonoBehaviour
{
    public int damage=4;

    [SerializeField]
    float dashDistance=0;
    [SerializeField]
    float dashTime=0;


    float timer;


    [SerializeField]
    float distanceBehind;



    Vector3 dashDirection;


    Rigidbody rb;


    List<Weapon> unitWeapons;

    Sword sword;
    Shield shield;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetWeaponReferences();
    }


    void GetWeaponReferences() {
        foreach (var obj in GetComponentsInChildren<Weapon>()) {
            if (obj is Sword)
            {
                sword = obj as Sword;
            }
            else if (obj is Shield) {
                shield = obj as Shield;
            }
        }
    
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


    public void AttackShield() {
        if (shield.HitsSomething()) {
            shield.OverLappedObject.GetComponent<Stats>()?.DealDamage(damage);


            //Destroy(shield.OverLappedObject);
            //Debug.Log("Hit Shield");
        }


       
    }

    public void AttackSword()
    {
        if (sword.HitsSomething())
        {
            sword.OverLappedObject.GetComponent<Stats>()?.DealDamage(damage);
            //Destroy(sword.OverLappedObject);
            //Debug.Log("Hit Sword");
            //sword.OverLappedObject.

        }
       
    }



}
