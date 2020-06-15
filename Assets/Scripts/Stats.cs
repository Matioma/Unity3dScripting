using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private int _health;


    public event Action OnDeath;

    void Awake()
    {
    }

    public int Health {
        get { return _health; }
        private set {     
            _health = value;
            if (_health <= 0) {
                Debug.Log("GG");
                _health = 0;
                Died();
                
            }
        }
    }

    public void DealDamage(int amount) {
        Health -= amount;
    
    }

    void Died() {
        OnDeath?.Invoke();
        DeathAnimation();
        Destroy(this.gameObject,5);
    }


    public void DeathAnimation() {
        GetComponent<Animator>()?.SetTrigger("Died");
    
    }

}
