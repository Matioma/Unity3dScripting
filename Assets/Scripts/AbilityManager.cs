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

    public Vector3 GetDashDirection() {
        return dashDirection;
    }

    public Vector3 LocalDashDirection{ get; set; }

    bool dashFinished = false;



    public event Action onDashStart;
    public event Action onDashEnd;




    Rigidbody rb;

    Sword sword;
    Shield shield;


    public event Action onSwordHit;
    public event Action onShieldHit;

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

        //while dashing add a force in a direction
        if (timer < dashTime)
        {
            rb.AddForce(dashDirection * dashDistance, ForceMode.Force);
            timer += Time.fixedDeltaTime;
        }
        else {
            if (dashFinished == false) {
                dashFinished = true;
                onDashEnd?.Invoke();
            }
        }
    }

    public void Update()
    {
        
    }


    public void DashLeft() {
        Dash(-transform.right);
        //LocalDashDirection = new Vector3(1, 0, 0);

    }
    public void DashRight() {
        Dash(transform.right);
        //LocalDashDirection = new Vector3(-1,0,0);
    }
    public void DashBack() {
        Dash(-transform.forward);
        //LocalDashDirection = new Vector3(0, 0, -1);
    }
    public void DashBehind()
    {
        if (Target == null) return;
        

        //Set the position behind the enemy
        transform.position = Target.position;
        transform.position += -Target.forward * distanceBehind; 


        
        Vector3 relativePosition = Target.position - transform.position; //get vector from target to player
        Quaternion rotationTowards=Quaternion.LookRotation(relativePosition); // Look towards target

        transform.rotation = Quaternion.Euler(0, rotationTowards.eulerAngles.y, 0);
    }
    void Dash(Vector3 direction)
    {
        //initializes Dashing in a specific direction
        dashFinished = false;

       
        timer = 0;
        dashDirection = direction;

        onDashStart?.Invoke();
    }
    public void AttackShield() {
        if (shield.HitsSomething()) {
            if (shield.OverLappedObject.tag != gameObject.tag)
            {
                var damage = GetComponent<Stats>().ShieldDamage;
                shield.OverLappedObject.GetComponent<Stats>()?.DealDamage(damage);
                onShieldHit?.Invoke();            
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

                onSwordHit?.Invoke();
            }
        }
    }
}
