using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveList : MonoBehaviour
{
    public Toggle objectivePrefab;

    [SerializeField]
    int distanceBetweenObjectives = 30;
    [SerializeField]
    Vector3 StartPostion = new Vector3();



    List<Toggle> displayedObjectives = new List<Toggle>();
     


    void Start()
    {
        foreach (var objective in FindObjectsOfType<Objective>()) {
            var toggleElement= AddObjective(objective);

            objective.onObjectiveCompleted += () => { toggleElement.isOn = objective.IsCompleted;};
        }
    }


    Toggle AddObjective(Objective objective) {
        var toggle = Instantiate(objectivePrefab, transform);
        displayedObjectives.Add(toggle);
        toggle.transform.localPosition = StartPostion + new Vector3(0,-displayedObjectives.Count * distanceBetweenObjectives ,0);

        toggle.isOn = objective.IsCompleted;
        toggle.GetComponentInChildren<Text>().text = objective.objectiveMessage;
        return toggle;
    }


    void UpdateToggleState(Toggle toggle, Objective objective) {
        toggle.isOn = objective.IsCompleted;
    }

}