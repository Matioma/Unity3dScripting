using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour
{
    public GameObject OverLappedObject{get; private set;}



    public bool HitsSomething() {
        if (OverLappedObject != null) {
            return true;
        }
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<Stats>() != null) {
            OverLappedObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == OverLappedObject) {
            OverLappedObject = null;
        }
    }
}
