using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    static LevelManager _instance;

    public static LevelManager Instance {
        get { return _instance; }
        set {
            if (_instance == null)
            {
                _instance = value;
            }
        }
    }
    public readonly List<Transform> enemies =new List<Transform>();



    public readonly List<Objective> Objectives = new List<Objective>();
    
    void Start()
    {
        Instance = this;
        foreach (var enemyController in FindObjectsOfType<EnemyController>()) {
            enemies.Add(enemyController.transform);
        }

        foreach (var objective in FindObjectsOfType<Objective>()) {
            Objectives.Add(objective);
        
        }
    }

    private void OnDestroy()
    {
        
        if (this == _instance) {
            _instance = null;
        }
    }



}
