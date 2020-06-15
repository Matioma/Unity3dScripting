using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sword : Weapon
{
    GameObject owner;

    //PlayerController playerController

    private void Awake()
    {
        //Debug.Log(PrefabUtility.GetPrefabInstanceHandle(this));

        //GetComponentInParent<PlayerController>();
    }



    void OnTriggerEnter(Collider other)
    {
        Debug.Log("GG");
    }

}
