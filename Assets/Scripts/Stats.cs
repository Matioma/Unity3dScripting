using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    
    public event Action OnDeath;
    public event Action OnHealthChange;


    [Header ("Combat")]
    [SerializeField]
    int  _shieldDamage;
    public int ShieldDamage {
        get { return _shieldDamage; }
        set { _shieldDamage = value; }
    }

    [SerializeField]
    int _swordDamage;
    public int SwordDamage
    {
        get { return _swordDamage; }
        set { _swordDamage = value; }
    }

    [Header("Stats")]
    [SerializeField]
    int _maxHealth;
    public int MaxHeath {
        get {return _maxHealth;}
    }
    [SerializeField]
    int _health;

    public int Health {
        get { return _health; }
        private set {
            _health = value;
            OnHealthChange?.Invoke();
            if (_health <= 0) {
                _health = 0;
                Died();
            }
        }
    }

    public bool IsAlive
    {
        get { return Health > 0; }
    }

    private void Awake()
    {
        Health = MaxHeath;
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
