using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TutorialObjective : Objective
{

    [SerializeField]
    TutorialObjective objectiveToAdd;


    void Start()
    {
    }


    protected override void ObjectiveIsDone() {
        Debug.Log("Objective");
        base.ObjectiveIsDone();
    }


}
