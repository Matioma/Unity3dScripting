using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(AbilityManager))]
public class EnemyController : BaseController
{
    [Header("EnemyController")]
    [SerializeField]
    Transform _target;

    NavMeshAgent navMeshAgent;

    Animator animator;

    [SerializeField]
    float attackRange = 10f;


    //float viewDistance =30;
    [SerializeField]
    float fieldOfView = 180;



    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (animator == null) {
            Debug.LogError("Animator component is not attached to " + gameObject.name);
        }


        _target = FindObjectOfType<PlayerConroller>().transform;

        if (navMeshAgent == null)
        {

            Debug.LogError("Nav agent component is not attached to " + gameObject.name);
        }
    }
    
    private void Update()
    {
        Movement();
        GoToTarget();

        if (canSee(_target)) {
            Attack();
        }
    }

    private void Movement()
    {
        Vector3 planeMovement = new Vector3(navMeshAgent.velocity.x, 0, navMeshAgent.velocity.z);

        if (planeMovement.magnitude >= 0.5f)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    void GoToTarget()
    {
        if (canSee(_target)) {
            Vector3 targetVector = _target.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

    void Attack() {
        Vector3 targetVector = _target.position - transform.position;

        if (targetVector.sqrMagnitude <= attackRange * attackRange) {

            animator.SetTrigger("Attack");
        }
    }
}
