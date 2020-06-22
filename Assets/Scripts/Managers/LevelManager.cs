using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
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
    public readonly List<Transform> enemies =new List<Transform>();



    public readonly List<Objective> objectives = new List<Objective>();
    


    private DelayedAction delayedAction;

    void Awake()
    {
        Instance = this;
        foreach (var enemyController in FindObjectsOfType<EnemyController>()) {
            enemies.Add(enemyController.transform);
        }

        foreach (var objective in FindObjectsOfType<Objective>()) {
            objectives.Add(objective);
            objective.onObjectiveCompleted += levelPassed;
        }
    }

    private void Update()
    {
        HandleDelayedAction();
    }


    void levelPassed() {
        if (areObjectivesFullfilled()) {
            SceneManager.LoadScene(targetScene);
            Debug.Log("Level Passed");
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
     


    void HandleDelayedAction()
    {
        if (delayedAction != null) {
            if (delayedAction.Update()) {
            }
            else{
                delayedAction = null;
            }
        }
    }

    public void OpenSceneDelayed(string Scene, float time) {
        delayedAction = new DelayedAction(time, () => { SceneManager.LoadScene(Scene); });
    }


}
