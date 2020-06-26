using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [Header("BaseController")]
    [SerializeField]protected float onGroundRayHeight=10;

    [SerializeField] protected float viewDistance=30;

    public event Action onStepping;



    public void Stepped() {
        onStepping?.Invoke();
    }



    protected bool isOnGround()
    {
        RaycastHit raycastHit;
        Debug.DrawRay(transform.position, Vector3.down);

        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, onGroundRayHeight))
        {
            return true;
        }
        return false;

    }
    protected bool canSee(Transform target)
    {
        if (target != null)
        {
            if (inFront(target))
            {
                RaycastHit raycast;
                Vector3 rayDirection = target.transform.position - transform.position;

                if (Physics.Raycast(transform.position, rayDirection, out raycast, viewDistance))
                {
                    if (raycast.transform.GetComponent<PlayerController>() != null)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    protected bool canSee(Transform target , Type type)
    {
        if (target != null)
        {
            if (inFront(target))
            {
                RaycastHit raycast;
                Vector3 rayDirection = target.transform.position - transform.position;

                if (Physics.Raycast(transform.position, rayDirection, out raycast, viewDistance))
                {
                    if(type == typeof(EnemyController))

                    if (raycast.transform.GetComponent<EnemyController>() != null)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    protected bool inFront(Transform target)
    {
        if (target != null)
        {
            Vector3 toTargetVector = target.position - transform.position;

            float dotProduct = Vector3.Dot(toTargetVector.normalized, transform.forward);
            if (dotProduct > 0)
            {
                return true;
            }
        }
        return false;
    }
}
