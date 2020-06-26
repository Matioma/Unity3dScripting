using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject MenuUi;
    [SerializeField]
    GameObject SettingsUI;

    [SerializeField]
    Slider volumeSliderElement;


    /// <summary>
    /// Swaps between MenuUi and Settings UI
    /// </summary>
    public void SwitchView() {
        MenuUi?.SetActive(!MenuUi.activeSelf);
        SettingsUI?.SetActive(!SettingsUI.activeSelf);
    }


    public void VolumeCanged() {
        GameSettings.Instance?.SetVolume(volumeSliderElement.value);
    }
}
