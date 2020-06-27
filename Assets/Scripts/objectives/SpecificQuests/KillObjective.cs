using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjective : TutorialObjective
{

    Stats target;


    private void Awake()
    {
        target = FindObjectOfType<EnemyController>().GetComponent<Stats>();
        //KillObjective killObjective =target.gameObject.AddComponent<KillObjective>();
        target.OnDeath += ObjectiveTask;
    }

    protected override void ObjectiveTask()
    {
        DoneOneObjective();
    }
}
