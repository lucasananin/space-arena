using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaPMod : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] GameObject _damageAreaPrefab = null;
    [SerializeField] bool _onHit = true;
    [SerializeField] bool _onDestroy = true;
    [SerializeField, ReadOnly] bool _hasSpawned = false;

    private void OnEnable()
    {
        _projectileBehaviour.onRaycastHit += TrySpawnArea;
        _projectileBehaviour.OnDestroy += TrySpawnArea;
    }

    private void OnDisable()
    {
        _projectileBehaviour.onRaycastHit -= TrySpawnArea;
        _projectileBehaviour.OnDestroy -= TrySpawnArea;
    }

    private void TrySpawnArea(RaycastHit2D _hit)
    {
        if (_hasSpawned) return;
        if (!_onHit) return;

        _hasSpawned = true;
        Instantiate(_damageAreaPrefab, _hit.point, Quaternion.identity);
    }

    private void TrySpawnArea()
    {
        if (_hasSpawned) return;
        if (!_onDestroy) return;

        _hasSpawned = true;
        Instantiate(_damageAreaPrefab, transform.position, Quaternion.identity);
    }
}
