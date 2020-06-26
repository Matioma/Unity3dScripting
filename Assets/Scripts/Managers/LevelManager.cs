﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public string targetScene="Level1";

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
    public readonly List<Transform> targets =new List<Transform>();
    public readonly List<Objective> objectives = new List<Objective>();
    public List<AbilityManager> abilityManagers = new List<AbilityManager>();


    private DelayedAction delayedAction;

    public event Action onObjectiveStateChange;

    void Awake()
    {
        Instance = this;



        foreach (var enemyController in FindObjectsOfType<Targetable>()) {
            targets.Add(enemyController.transform);
        }
        foreach (var objective in FindObjectsOfType<Objective>()) {
            objectives.Add(objective);

            objective.onObjectiveCompleted += PassLevel;
        }
        foreach (var manager in FindObjectsOfType<AbilityManager>()) {
            abilityManagers.Add(manager);

        }

    }

    private void Update()
    {
        HandleDelayedActions();
    }


    /// <summary>
    /// Action to be done on finishing all objectives
    /// </summary>
    void PassLevel() {
        if (areObjectivesFullfilled()) {
            OpenSceneDelayed(targetScene,1f);
        }    
    }
    bool areObjectivesFullfilled() {
        foreach (var objective in objectives) {
            if (objective.IsCompleted == false) {
                return false;
            }
        }
        return true;
    }
    void OnDestroy()
    {
        
        if (this == _instance) {
            _instance = null;
        }
    }
     


    void HandleDelayedActions()
    {
        if (delayedAction != null)
        {
            if (delayedAction.Update())
            {
            }
            else
            {
                delayedAction = null;
            }
        }
    }



    /// <summary>
    /// Calls load scene with a delay
    /// </summary>
    /// <param name="Scene"></param>
    /// <param name="time"></param>
    public void OpenSceneDelayed(string Scene, float time) {
        delayedAction = new DelayedAction(time, () => { SceneManager.LoadScene(Scene); });
    }
}
