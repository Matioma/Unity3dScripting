using System;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(Rigidbody))]
public class AbilityManager : MonoBehaviour
{
   

    [Header("Simple Dash")]
    [SerializeField]
    float dashDistance = 0;
    [SerializeField]
    float dashTime = 0;
    float timer;



    [Header("Dash Behind")]
    [SerializeField]
    float distanceBehind = 1;

    Vector3 dashDirection;




    Rigidbody rb;

    Sword sword;
    Shield shield;


    [SerializeField]
    Transform _target;
    public Transform Target {
        get { return _target; }
        set {
            onTargetChange();
            if (Target != null) {
                Target?.GetComponentInChildren<TargetIndicator>().Deselect();
            }            
            _target = value;
            if (Target != null)
            {
                Target?.GetComponentInChildren<TargetIndicator>().Select();
            }
        }
    }

    public Action onTargetChange;




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

    public void Update()
    {
        
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
    public void DashBehind()
    {
        if (Target == null) return;

        transform.position = Target.position;
        transform.position += -Target.forward * distanceBehind;

        Vector3 relativePosition = Target.position - transform.position;
        Quaternion rotationTowards=Quaternion.LookRotation(relativePosition);

        transform.rotation = Quaternion.Euler(0, rotationTowards.eulerAngles.y, 0);
    }
    void Dash(Vector3 direction)
    {
        timer = 0;
        dashDirection = direction;


        //Debug.DrawLine(transform.position, transform.position+)
        //direction += new Vector3(0, 0.3f);
        //direction.Normalize();
        //Debug.DrawLine(transform.position, transform.position + direction * 100, Color.green, 10f);
        //rb.AddForce((direction)* 10, ForceMode.Impulse);
    }
    public void AttackShield() {
        if (shield.HitsSomething()) {
            if (shield.OverLappedObject.tag != gameObject.tag)
            {
                var damage = GetComponent<Stats>().ShieldDamage;
                shield.OverLappedObject.GetComponent<Stats>()?.DealDamage(damage);
            }
        }
    }
    public void AttackSword()
    {
        if (sword.HitsSomething())
        {
            if (sword.OverLappedObject.tag != gameObject.tag) {
                var damage = GetComponent<Stats>().SwordDamage;
                sword.OverLappedObject.GetComponent<Stats>()?.DealDamage(damage);
            }
           
        }
    }

}
