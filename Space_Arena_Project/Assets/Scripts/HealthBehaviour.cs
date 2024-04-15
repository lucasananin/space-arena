using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] int _maxHealth = 100;
    [SerializeField, ReadOnly] int _currentHealth = 0;

    public event System.Action onDead = null;
    public event System.Action onDamageTaken = null;

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
            onDead?.Invoke();
        }
        else
        {
            onDamageTaken?.Invoke();
        }
    }

    public void RestoreHealth()
    {
        _currentHealth = _maxHealth;
    }
}
