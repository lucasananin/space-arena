using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedExplosionPMod : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] SimpleExplosionVfx _vfxPrefab = null;
    [SerializeField] GameObject _particleSystemPrefab = null;

    private void OnValidate()
    {
        _projectileBehaviour = GetComponent<ProjectileBehaviour>();
    }

    private void OnEnable()
    {
        _projectileBehaviour.OnDestroyTimerEnd += Explode;
    }

    private void OnDisable()
    {
        _projectileBehaviour.OnDestroyTimerEnd -= Explode;
    }

    private void Explode()
    {
        var _myPosition = transform.position;
        _projectileBehaviour.Explode(_myPosition);

        if (_vfxPrefab != null)
        {
            var _vfxInstance = Instantiate(_vfxPrefab, _myPosition, Quaternion.identity);
            _vfxInstance.Init(_projectileBehaviour.GetExplosionRadius());
        }

        if (_particleSystemPrefab != null)
        {
            Instantiate(_particleSystemPrefab, _myPosition, Quaternion.identity);
        }
    }
}
