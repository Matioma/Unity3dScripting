
using System;
using System.ComponentModel;
using UnityEngine;

public class QuestLine : MonoBehaviour
{
    public event Action onObjectiveEnd;


    [SerializeField]
    TutorialObjective[] ObjectiveOrder;


    int indexQuestToAdd = 0;

    void Start()
    {
        if (ObjectiveOrder != null && ObjectiveOrder.Length != 0) {
            Instantiate(ObjectiveOrder[indexQuestToAdd], transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddNextQuest() {
        indexQuestToAdd++;

        if (indexQuestToAdd < ObjectiveOrder.Length) {
            var objective =Instantiate(ObjectiveOrder[indexQuestToAdd], transform);
            LevelManager.Instance.AddNewObjective(objective);
        }


    }
}
