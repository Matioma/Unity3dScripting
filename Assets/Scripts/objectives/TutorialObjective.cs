using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public abstract class TutorialObjective : Objective
{
    ObjectiveList objectiveList;
    QuestLine questLine;

    protected void Start()
    {
        questLine = transform.parent.GetComponent<QuestLine>();
        //Debug.Log(transform.parent.name);


        objectiveList = FindObjectOfType<ObjectiveList>();
        objectiveList?.AddObjective(GetComponent<Objective>());
    }

    protected override void DoneOneObjective() {
        if (IsCompleted) {
            return;
        }
        questLine.AddNextQuest();

        ObjectiveIsDone();
    }




    protected abstract void ObjectiveTask();

}
