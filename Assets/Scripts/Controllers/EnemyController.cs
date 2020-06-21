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



    bool playerDetected = false;

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
        MovementAnimation();
        GoToTarget();

        if (canSee(_target)) {

            Attack();
        }
    }
    



    private void MovementAnimation()
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
        if (_target != null)
        {
            if (canSee(_target) && !playerDetected)
            {
                playerDetected = true;

            }

            if (playerDetected)
            {
                Vector3 targetVector = _target.transform.position;
                navMeshAgent.SetDestination(targetVector);
            }
        }
    }

    void Attack() {

        if (_target == null) {
            return;
        }

        Vector3 targetVector = _target.position - transform.position;
        if (targetVector.sqrMagnitude <= attackRange * attackRange) {

            animator.SetTrigger("Attack");
        }
    }


    private void OnDestroy()
    {
        if (LevelManager.Instance == null) {
            return;
        }
        if (LevelManager.Instance.enemies.Contains(transform)) {
            LevelManager.Instance.enemies.Remove(transform);
        }
        
    }
}
