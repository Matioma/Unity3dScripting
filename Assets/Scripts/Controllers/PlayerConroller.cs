
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AbilityManager))]
public class PlayerConroller : BaseController
{
    [Header("PlayerController")]
    [SerializeField]
    float MaxSpeed;
    [SerializeField]
    float rotationSpeed=10;



    Animator animator;
    Rigidbody rigidbody;



    [SerializeField]
    float consecutiveInputTime = 0;
    float consecutiveInputTimer;
    bool pressedBackTwice;


    [SerializeField]
    float angleToSelectTarget = 10f;


    private void Awake()
    { 
      
    }
    void Start()
    {
        consecutiveInputTimer = 0;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        GetComponent<AbilityManager>().onTargetChange += targetChanged;


        GetComponent<Stats>().OnDeath += () => { LevelManager.Instance.OpenSceneDelayed(GameSettings.Instance.DefeatLevel, 1.5f);};
    }

    public void  Update()
    {

        if (!GetComponent<Stats>().IsAlive) {
            return;
        }
        pressedBackTwice = IsDashBackTriggered();
        consecutiveInputTimer -= Time.deltaTime;

        if (isOnGround()) {
            if (Input.GetButtonDown("DashLeft"))
            {
                GetComponent<AbilityManager>().DashLeft();
            }
            if (Input.GetButtonDown("DashRight"))
            {
                GetComponent<AbilityManager>().DashRight();
            }
            if (pressedBackTwice)
            {
                GetComponent<AbilityManager>().DashBack();
                pressedBackTwice = false;
            }
            MoveForward();
            
        }
        Attack();
        Rotate();
        if (Input.GetButtonDown("DashBehindTarget"))
        {
            GetComponent<AbilityManager>().DashBehind();
        }
        TargetInFront();
    }

    void MoveForward()
    {
        var forwardInput = Input.GetAxis("Vertical");

        if (forwardInput != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        rigidbody.velocity =new Vector3(0,rigidbody.velocity.y,0)+ transform.forward * forwardInput * MaxSpeed;
    }
    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
    }
    void Rotate() {
        float horizontalInput = Input.GetAxis("Horizontal");      
        transform.Rotate(new Vector3(0, horizontalInput * rotationSpeed * Time.deltaTime,0));
    }
    bool IsDashBackTriggered() {
        if (Input.GetButtonDown("S"))
        {
            if (consecutiveInputTimer < 0)
            {
                consecutiveInputTimer = consecutiveInputTime;
            }
            else {
                consecutiveInputTimer = -1;
                return true;
            }
        }
        return false;
    }

    void TargetInFront() {
        if (LevelManager.Instance == null) {
            return;
        }
        foreach (var obj in LevelManager.Instance.enemies) {
            if (inFront(obj)) {
                Vector3 forwardVector = new Vector3(transform.forward.x, 0, transform.forward.z);
                Vector3 directionToTargetNotmalized = (obj.transform.position - transform.position).normalized;
                Vector3 directionToTargetWithoutY = new Vector3(directionToTargetNotmalized.x, 0, directionToTargetNotmalized.z);

                if (Math.Acos(Vector3.Dot(forwardVector, directionToTargetWithoutY)) <= Mathf.Deg2Rad*angleToSelectTarget)
                {
                    var abilityManger = GetComponent<AbilityManager>();
                    GetComponent<AbilityManager>().Target = obj;
                    break;
                }
            }
        }
    }


    void targetChanged() { 
        
    
    }
}
