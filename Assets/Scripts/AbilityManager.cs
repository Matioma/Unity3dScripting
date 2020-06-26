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

    public Sword sword { get; private set; }
    public Shield shield { get; private set; }


    public event Action onSwordHit;
    public event Action onShieldHit;
    public event Action onWeaponSwinging;

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



    [SerializeField]
    float dashBehindCoolDown;


    Timer dashBehindTimer = new Timer();
    Timer dashLeftTimer = new Timer();



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


    private void Update()
    {
        dashBehindTimer.Update();
        dashLeftTimer.Update();
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

        //check if the cooldown ended;
        if (!dashBehindTimer.HasEnded) {
            return;
        }
        dashBehindTimer.SetTimer(dashBehindCoolDown);


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

        if (!dashLeftTimer.HasEnded)
        {
            return;
        }
        dashLeftTimer.SetTimer(dashBehindCoolDown);


        timer = 0;
        dashDirection = direction;

        onDashStart?.Invoke();
    }
    public void AttackShield() {
        if (shield == null) {
            return;
        }
        if (shield.HitsSomething()) {

            //If does not hit object of same tye
            if (shield.OverLappedObject.tag != gameObject.tag)
            {
                var damage = GetComponent<Stats>().ShieldDamage;
                shield.OverLappedObject.GetComponent<Stats>()?.DealDamage(damage);

                onShieldHit?.Invoke();
            }
            
        }
        else       
        {
            onWeaponSwinging?.Invoke();
        }
    }
    public void AttackSword()
    {
        if (sword == null) {
            return;
        }
        if (sword.HitsSomething())
        {
            if (sword.OverLappedObject.tag != gameObject.tag) {
                var damage = GetComponent<Stats>().SwordDamage;
                sword.OverLappedObject.GetComponent<Stats>()?.DealDamage(damage);
           
                onSwordHit?.Invoke();
            }
        }
        else
        {
            onWeaponSwinging?.Invoke();
        }
    }
}
