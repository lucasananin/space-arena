using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class HealthBehaviour : MonoBehaviour
{
    [SerializeField] int _maxHealth = 100;
    [SerializeField, ReadOnly] int _currentHealth = 0;

    public event System.Action onDamageTaken = null;
    public event System.Action onDead = null;
    //public UnityEvent onDamageTaken_UEvent = null;
    //public UnityEvent onDead_UEvent = null;

    private void Awake()
    {
        RestoreHealth();
    }

    public void TakeDamage(int _value)
    {
        _currentHealth -= _value;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDead();
            onDead?.Invoke();
        }
        else
        {
            OnDamageTaken();
            onDamageTaken?.Invoke();
        }
    }

    public void RestoreHealth()
    {
        _currentHealth = _maxHealth;
    }

    public bool IsAlive()
    {
        return _currentHealth > 0;
    }

    [Button]
    private void TakeDamage()
    {
        TakeDamage(1);
    }

    protected abstract void OnDead();
    protected abstract void OnDamageTaken();
}
