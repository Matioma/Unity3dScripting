using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAction
{
    float time;

    Action onTimerEnd;

    public DelayedAction(float pTime , Action onTimerEnd)
    {
        time = pTime;
        this.onTimerEnd = onTimerEnd;
    }

   

    public bool Update()
    {
        time -= Time.deltaTime;

        if (time <= 0) {
            onTimerEnd?.Invoke();
            return false;
        }
        return true;
    }

    public void Dispose()
    {
        onTimerEnd = null;
    }

}
