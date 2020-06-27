using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ReachDestination : TutorialObjective
{
    [SerializeField]
    float triggerRange;

    [SerializeField]
    GameObject destinationObject; //INdicator object
    [SerializeField]
    Vector3 destinationPosition;

    PlayerController player;
    Transform destination;


    private void Awake()
    {
        player =GameObject.FindObjectOfType<PlayerController>();


        if (destinationObject != null) {
            var indicatorObject = Instantiate(destinationObject);
            destination = indicatorObject.transform;
            destination.position = destinationPosition;
        }
    }

    protected override void ObjectiveTask()
    {
        if (destination == null)
        {
            Debug.Log("Destination not set");

        }

        Vector3 distanceVector = destination.position - player.transform.position;

        if (distanceVector.sqrMagnitude <= triggerRange)
        {
            this.gameObject.SetActive(false);
            destination.gameObject.SetActive(false);
            DoneOneObjective();
        }
    }

    private void Update()
    {
        ObjectiveTask();
    }




}
