using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUnit : MonoBehaviour
{
    [SerializeField]
    AudioClip swordHitSound;
    [SerializeField]
    AudioClip shieldHitSound;

    [SerializeField]
    AudioClip weaponSwingSound;

    [SerializeField]
    AudioClip[] stepSound;

    [SerializeField]
    AudioClip damageSound;

    Sword sword;
    Shield shield;



    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();


        GetComponent<Stats>().onDamgeReceive += AudioUnit_OnDamageReceive;
        

        SetTheOnHitSounds();
    }

    private void AudioUnit_OnDamageReceive()
    {
        //audioSource.PlayOneShot(damageSound);
        //throw new System.NotImplementedException();
    }

    private void SetTheOnHitSounds()
    {
        sword = GetComponentInChildren<Sword>();
        shield = GetComponentInChildren<Shield>();



        // Subscribe all sound effects
        var abilityManager = GetComponent<AbilityManager>();
        if (abilityManager != null)
        {
            abilityManager.onSwordHit += () => { sword.GetComponent<AudioSource>()?.PlayOneShot(swordHitSound); };
            abilityManager.onShieldHit += () => { shield.GetComponent<AudioSource>()?.PlayOneShot(shieldHitSound); };

            abilityManager.onSwordHit += () => { sword.GetComponent<AudioSource>()?.PlayOneShot(weaponSwingSound); };
            abilityManager.onWeaponSwinging += () => { shield.GetComponent<AudioSource>()?.PlayOneShot(weaponSwingSound); };
        }



        var Controller = GetComponent<BaseController>();
        if (Controller != null) {
            Controller.onStepping += Controller_onStepping;
        
        }

    }

    private void Controller_onStepping()
    {
        if (stepSound == null || stepSound.Length==0) {
            return;
        }
        int index = Random.Range(0, stepSound.Length);
     

        if (audioSource != null) {
            audioSource.pitch = Random.Range(0.5f, 1f);

            audioSource.PlayOneShot(stepSound[index]);
        }

        
    }
}
