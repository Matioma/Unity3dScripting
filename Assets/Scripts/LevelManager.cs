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

    
    void Start()
    {
        Instance = this;
        foreach (var obj in FindObjectsOfType<EnemyController>()) {
            enemies.Add(obj.transform);
        }
    }

    private void OnDestroy()
    {
        
        if (this == _instance) {
            _instance = null;
        }
    }
}
