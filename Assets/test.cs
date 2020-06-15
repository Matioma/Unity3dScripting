using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var stats = other.GetComponent<Stats>();

        if (stats != null) {
            stats.DealDamage(3);

        }
    }
}
