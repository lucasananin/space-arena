using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieVfxSpawner : MonoBehaviour
{
    [SerializeField] HealthBehaviour _health = null;
    [SerializeField] ParticleSystem _deadVfx = null;

    private void OnValidate()
    {
        if (_health is null)
            _health = GetComponent<HealthBehaviour>();
    }

    private void OnEnable()
    {
        _health.OnDead += Spawn;
    }

    private void OnDisable()
    {
        _health.OnDead -= Spawn;
    }

    private void Spawn()
    {
        var _rotation = GeneralMethods.GetLookRotation(transform.position, _health.LastDamageModel.PointHit);
        Instantiate(_deadVfx, transform.position, _rotation);
    }
}
