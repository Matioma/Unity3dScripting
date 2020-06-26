using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float timer;


    public Timer()
    {   
    }

    public bool HasEnded { get {return timer < 0; } }



    // Update is called once per frame
    public void Update()
    {
        timer -= Time.deltaTime;
    }

    public void SetTimer(float pLength) {
        if(pLength>0)
            timer = pLength;

    }
}
