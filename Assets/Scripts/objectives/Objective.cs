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
        if(GetComponent<Stats>() != null)
        {
            GetComponent<Stats>().OnDeath += ObjectiveIsDone;
        }
        
    }

    protected virtual void ObjectiveIsDone() {
        IsCompleted = true;
        onObjectiveCompleted?.Invoke();    
    }

}
