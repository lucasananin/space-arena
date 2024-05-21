using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLootOnDead : LootDropper
{
    [SerializeField] HealthBehaviour _healthBehaviour = null;

    private void OnValidate()
    {
        _healthBehaviour = GetComponent<HealthBehaviour>();
    }

    private void OnEnable()
    {
        _healthBehaviour.onDead += _healthBehaviour_onDead;
    }

    private void OnDisable()
    {
        _healthBehaviour.onDead -= _healthBehaviour_onDead;
    }

    private void _healthBehaviour_onDead()
    {
        Drop();
    }
}
