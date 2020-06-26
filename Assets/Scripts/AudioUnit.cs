using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUnit : MonoBehaviour
{
    [SerializeField]
    AudioClip swordHit;
    [SerializeField]
    AudioClip shieldHit;



    Sword sword;
    Shield shield;




    void Start()
    {

        SetTheOnHitSounds();
    }


    private void SetTheOnHitSounds()
    {
        sword = GetComponentInChildren<Sword>();
        shield = GetComponentInChildren<Shield>();


        var swordAudioSource = sword.GetComponent<AudioSource>();
        if (swordAudioSource != null)
        {
            swordAudioSource.clip = swordHit;
        }

        var shieldAudioSource = shield.GetComponent<AudioSource>();
        if (shieldAudioSource != null)
        {
            shieldAudioSource.clip = shieldHit;
        }

        var abilityManager = GetComponent<AbilityManager>();
        if (abilityManager != null)
        {
            abilityManager.onShieldHit += () => { sword.GetComponent<AudioSource>()?.PlayOneShot(swordHit); };
            abilityManager.onShieldHit += () => { shield.GetComponent<AudioSource>()?.PlayOneShot(shieldHit); };
        }
    }
}
