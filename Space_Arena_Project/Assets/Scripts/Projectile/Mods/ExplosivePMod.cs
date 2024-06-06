using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivePMod : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] SimpleExplosionVfx _simpleExplosionPrefab = null;
    [SerializeField] GameObject _particleSystemPrefab = null;
    [SerializeField] bool _onHit = true;
    [SerializeField] bool _onStop = true;
    [SerializeField] bool _onTimerEnd = true;

    private void OnEnable()
    {
        _projectileBehaviour.onRaycastHit += ExplodeByHit;
        _projectileBehaviour.OnDestroy_Stop += ExplodeByStop;
        _projectileBehaviour.OnDestroy_TimerEnd += ExplodeByTimer;
    }

    private void OnDisable()
    {
        _projectileBehaviour.onRaycastHit -= ExplodeByHit;
        _projectileBehaviour.OnDestroy_Stop -= ExplodeByStop;
        _projectileBehaviour.OnDestroy_TimerEnd -= ExplodeByTimer;
    }

    private void ExplodeByHit(RaycastHit2D _raycastHit)
    {
        if (_onHit)
            Explode(_raycastHit.point);
    }

    private void ExplodeByStop()
    {
        if (_onStop)
            Explode(transform.position);
    }

    private void ExplodeByTimer()
    {
        if (_onTimerEnd)
            Explode(transform.position);
    }

    private void Explode(Vector3 _position)
    {
        _projectileBehaviour.Explode(_position);

        if (_simpleExplosionPrefab != null)
        {
            var _vfxInstance = Instantiate(_simpleExplosionPrefab, _position, Quaternion.identity);
            _vfxInstance.Init(_projectileBehaviour.GetExplosionRadius());
        }

        if (_particleSystemPrefab != null)
        {
            Instantiate(_particleSystemPrefab, _position, Quaternion.identity);
        }
    }
}
