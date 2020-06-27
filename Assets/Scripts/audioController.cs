using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    AudioSource audioSource;

    float initialVolume;


    private void Start()
    {
        audioSource=GetComponent<AudioSource>();

        if (audioSource != null) {
            initialVolume = audioSource.volume;
        }

        audioSource.volume = initialVolume * GameSettings.volume;

    }


    public void SetVolume(float fraction)
    {
        audioSource.volume = initialVolume * fraction;
    }
}
