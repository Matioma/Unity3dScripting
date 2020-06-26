using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMovement : TutorialObjective
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical")!=0 || Input.GetAxis("Horizontal")!=0)
        {
            DoneOneObjective();
        }
    }
}
