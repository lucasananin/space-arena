using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthBehaviour : MonoBehaviour
{
    [SerializeField] protected bool _canTakeDamage = true;
    [SerializeField] protected int _maxHealth = 100;
    [SerializeField, ReadOnly] protected int _currentHealth = 0;
    [SerializeField, ReadOnly] protected DamageModel _lastDamageModel = null;

    public DamageModel LastDamageModel { get => _lastDamageModel; private set => _lastDamageModel = value; }

    public event System.Action onDamageTaken = null;
    public event System.Action onDead = null;

    private void Awake()
    {
        RestoreHealth();
    }

    [Button]
    public void TakeDamage()
    {
        var _damageModel = new DamageModel(null, transform.position, 1);
        TakeDamage(_damageModel);
    }

    public void TakeDamage(DamageModel _damageModel)
    {
        _lastDamageModel = _damageModel;
        _currentHealth -= _damageModel.Value;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            onDead?.Invoke();
            OnDead();
        }
        else
        {
            onDamageTaken?.Invoke();
            OnDamageTaken();
        }

        if (!_canTakeDamage)
        {
            RestoreHealth();
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

    protected abstract void OnDead();
    protected abstract void OnDamageTaken();
}
