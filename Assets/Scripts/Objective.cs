using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public bool IsCompleted { get; private set; }


    public Action onObjectiveCompleted;
    public string objectiveMessage;

    private void Start()
    {
        onObjectiveCompleted += () => { IsCompleted = true; };
        GetComponent<Stats>().OnDeath += onObjectiveCompleted;
    }


    private void OnDestroy()
    {
    }


}
