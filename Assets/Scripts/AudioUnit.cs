using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUnit : MonoBehaviour
{
    [SerializeField]
    AudioClip swordHit;
    [SerializeField]
    AudioClip shieldHit;

    [SerializeField]
    AudioClip weaponSwing;


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



        // Subscribe all sound effects
        var abilityManager = GetComponent<AbilityManager>();
        if (abilityManager != null)
        {
            abilityManager.onSwordHit += () => { sword.GetComponent<AudioSource>()?.PlayOneShot(swordHit); };
            abilityManager.onShieldHit += () => { shield.GetComponent<AudioSource>()?.PlayOneShot(shieldHit); };

            abilityManager.onSwordHit += () => { sword.GetComponent<AudioSource>()?.PlayOneShot(weaponSwing); };
            abilityManager.onWeaponSwinging += () => { shield.GetComponent<AudioSource>()?.PlayOneShot(weaponSwing); };
        }
    }
}
