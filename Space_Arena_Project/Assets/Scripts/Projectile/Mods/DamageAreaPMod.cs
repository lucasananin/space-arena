using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaPMod : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectile = null;
    [SerializeField] DamageArea _prefab = null;
    [SerializeField] bool _onHit = true;
    [SerializeField] bool _onStop = true;
    [SerializeField] bool _onTimerEnd = true;

    private void OnEnable()
    {
        _projectile.onRaycastHit += SpawnAreaByHit;
        _projectile.OnDestroy_Stop += SpawnAreaByStop;
        _projectile.OnDestroy_TimerEnd += SpawnAreaByTimer;
    }

    private void OnDisable()
    {
        _projectile.onRaycastHit -= SpawnAreaByHit;
        _projectile.OnDestroy_Stop -= SpawnAreaByStop;
        _projectile.OnDestroy_TimerEnd -= SpawnAreaByTimer;
    }

    private void SpawnAreaByHit(RaycastHit2D _hit)
    {
        if (_onHit)
        {
            Spawn(_hit.point);
        }
    }

    private void SpawnAreaByStop()
    {
        if (_onStop)
        {
            Spawn(transform.position);
        }
    }

    private void SpawnAreaByTimer()
    {
        if (_onTimerEnd)
        {
            Spawn(transform.position);
        }
    }

    private void Spawn(Vector3 _position)
    {
        var _instance = Instantiate(_prefab, _position, Quaternion.identity);
        _instance.Init(_projectile.ShootModel);
    }
}
