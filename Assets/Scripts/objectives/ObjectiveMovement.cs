using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMovement : TutorialObjective
{
    protected override void ObjectiveTask()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            DoneOneObjective();
        }
    }


    // Update is called once per frame
    void Update()
    {
        ObjectiveTask();
    }



    
}
