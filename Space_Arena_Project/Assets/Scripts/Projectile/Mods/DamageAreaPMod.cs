using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaPMod : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] GameObject _damageAreaPrefab = null;
    [SerializeField] bool _onHit = true;
    [SerializeField] bool _onStop = true;
    [SerializeField] bool _onTimerEnd = true;

    private void OnEnable()
    {
        _projectileBehaviour.onRaycastHit += SpawnAreaByHit;
        _projectileBehaviour.OnDestroy_Stop += SpawnAreaByStop;
        _projectileBehaviour.OnDestroy_TimerEnd += SpawnAreaByTimer;
    }

    private void OnDisable()
    {
        _projectileBehaviour.onRaycastHit -= SpawnAreaByHit;
        _projectileBehaviour.OnDestroy_Stop -= SpawnAreaByStop;
        _projectileBehaviour.OnDestroy_TimerEnd -= SpawnAreaByTimer;
    }

    private void SpawnAreaByHit(RaycastHit2D _hit)
    {
        if (_onHit)
            Instantiate(_damageAreaPrefab, _hit.point, Quaternion.identity);
    }

    private void SpawnAreaByStop()
    {
        if (_onStop)
            Instantiate(_damageAreaPrefab, transform.position, Quaternion.identity);
    }

    private void SpawnAreaByTimer()
    {
        if (_onTimerEnd)
            Instantiate(_damageAreaPrefab, transform.position, Quaternion.identity);
    }
}
