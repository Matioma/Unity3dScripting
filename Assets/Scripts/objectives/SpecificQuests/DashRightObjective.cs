using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRightObjective : TutorialObjective
{
    protected override void ObjectiveTask()
    {
        if (Input.GetButtonDown("DashRight"))
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
