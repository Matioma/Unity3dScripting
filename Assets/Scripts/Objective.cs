using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{

    public Action onObjectiveConpleted;

    public string objectiveMessage;

    private void OnDestroy()
    {
        onObjectiveConpleted?.Invoke();
        Debug.Log("Objective achieved "  + objectiveMessage);
    }
}
