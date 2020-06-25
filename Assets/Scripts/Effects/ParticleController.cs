using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

    ParticleSystem particleSystem;

    [SerializeField]
    Vector3 velocityVector = new Vector3(10,0,0);

    [SerializeField]
    float particlesSpeed = 10f;



    AbilityManager abilityManager;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
        //particleSystem.startSpeed = particlesSpeed;

        abilityManager = transform.parent.GetComponent<AbilityManager>();
        if (abilityManager != null)
        {
            abilityManager.onDashStart += StartParticleSystem;
            abilityManager.onDashStart += StopParticles;
        }
        else {
            Debug.Log("Particle System is missing abilitymanager in parent " + transform.name);
        }
    }
    
    
    private void StartParticleSystem()
    {
        
        var particleSystemForce = particleSystem.forceOverLifetime;

        Vector3 dashDirection = abilityManager.GetDashDirection() * particlesSpeed;
        

        particleSystemForce.x = -dashDirection.x;
        particleSystemForce.y = -dashDirection.y;
        particleSystemForce.z = -dashDirection.z;

        particleSystem.Play();
    }

    private void StopParticleSystem() {      
        particleSystem.Stop();
    }

    private void StopParticles() {
        Invoke("StopParticleSystem", 0.5f);
    }
}
