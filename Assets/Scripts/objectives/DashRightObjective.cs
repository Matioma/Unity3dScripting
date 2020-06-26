using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRightObjective : TutorialObjective
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("DashRight"))
        {
            DoneOneObjective();
        }
    }
}
