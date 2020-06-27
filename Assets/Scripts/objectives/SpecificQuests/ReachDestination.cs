using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachDestination : TutorialObjective
{
    [SerializeField]
    GameObject destinationObject;
    
    [SerializeField]
    Vector3 destinationPosition;



    private void Awake()
    {
        
    }

    protected override void ObjectiveTask()
    {
        DoneOneObjective();
    }

    private void Update()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerController>() != null) {
            ObjectiveTask();
        }
    }


}
