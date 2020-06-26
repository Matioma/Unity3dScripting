using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    



    private static GameSettings _instance = null;
    public static GameSettings Instance { 
        get { return _instance; } 
        private set{_instance =value;} 
    }


    

    





    [SerializeField]
    [Header("Level Data")]
    string defeatLevel;
    public string DefeatLevel { get { return defeatLevel; } }



    public static float volume;
    public void SetVolume(float value)
    {
        if (value == volume)
        {
            return;
        }
        volume = value;

        foreach (var audioController in GameObject.FindObjectsOfType<audioController>())
        {
            audioController.SetVolume(volume);
        }
    }

    /// <summary>
    /// Sets the volume of all audio Sources
    /// </summary>
    public void UpdateSoundVolume()
    {
        foreach (var audioController in GameObject.FindObjectsOfType<audioController>())
        {
            audioController.SetVolume(volume);
        }
    }




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
}
