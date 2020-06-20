using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AbilityManager : MonoBehaviour
{
    [Header ("Simple Dash")]
    [SerializeField]
    float dashDistance=0;
    [SerializeField]
    float dashTime=0;
    float timer;



    [Header("Dash Behind")]
    [SerializeField]
    float distanceBehind =1;

    Vector3 dashDirection;

   


    Rigidbody rb;

    Sword sword;
    Shield shield;


    Transform target;


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
        if (target == null) return;

        transform.position = target.position;
        transform.position += -target.forward * distanceBehind;

        Vector3 relativePosition = target.position - transform.position;
        Quaternion rotationTowards=Quaternion.LookRotation(relativePosition);

        transform.rotation = Quaternion.Euler(0, rotationTowards.eulerAngles.y, 0);
    }
    void Dash(Vector3 direction)
    {
        timer = 0;
        dashDirection = direction;
    }
    public void AttackShield() {

        if (shield.HitsSomething()) {
            var damage = GetComponent<Stats>().ShieldDamage; 
            shield.OverLappedObject.GetComponent<Stats>()?.DealDamage(damage);
        }
    }
    public void AttackSword()
    {
        if (sword.HitsSomething())
        {
            var damage = GetComponent<Stats>().SwordDamage;
            sword.OverLappedObject.GetComponent<Stats>()?.DealDamage(damage);
        }
    }
}
