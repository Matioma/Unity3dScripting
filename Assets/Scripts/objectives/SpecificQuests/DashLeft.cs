﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashLeft : TutorialObjective
{
    protected override void ObjectiveTask()
    {
        if (Input.GetButtonDown("DashLeft"))
        {
            DoneOneObjective();
        }
    }


    // Start is called before the first frame update
    //void Start()
    //{
    //    base.Start();
    //    objectiveMessage = "Dash Left by pressing Q";
    //}

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("DashLeft")) {
        //    DoneOneObjective();
        //}    

        ObjectiveTask();
    }
}
