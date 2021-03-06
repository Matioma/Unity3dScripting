﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    public Stats stats;


    private void Start()
    {
            stats = FindObjectOfType<PlayerController>().GetComponent<Stats>();
            stats.onHealthChange += updateUI;
    }

    private void OnDestroy()
    {
        if(stats!=null)
            stats.onHealthChange -= updateUI;
    }
    void updateUI()
    {
        float scaleX = (float)stats.Health / stats.MaxHeath;

        if (scaleX > 0)
        {
            transform.localScale = new Vector3(scaleX, 1, 1);
        }
        else {
            transform.localScale = new Vector3(0, 1, 1);
        }
    }

}
