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


    public Toggle AddObjective(Objective objective) {

        // Creates a new togle item
        var toggle = Instantiate(objectivePrefab, transform);
        displayedObjectives.Add(toggle);


        //Set toggle item at appropriete position
        toggle.transform.localPosition = StartPostion + new Vector3(0,-displayedObjectives.Count * distanceBetweenObjectives ,0);

        toggle.isOn = objective.IsCompleted;
        //Add quest Message
        toggle.GetComponentInChildren<Text>().text = objective.objectiveMessage;
        return toggle;
    }


    void UpdateToggleState(Toggle toggle, Objective objective) {
        toggle.isOn = objective.IsCompleted;
    }

}