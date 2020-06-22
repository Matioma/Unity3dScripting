using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public bool IsCompleted { get; private set; }


    public event Action onObjectiveCompleted;
    public string objectiveMessage;

    private void Start()
    {
        GetComponent<Stats>().OnDeath += ObjectiveIsDone;
    }

    void ObjectiveIsDone() {
        IsCompleted = true;
        onObjectiveCompleted?.Invoke();    
    }

}
