using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedExplosionPMod : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] SimpleExplosionVfx _vfxPrefab = null;

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

        SimpleExplosionVfx _vfxInstance = Instantiate(_vfxPrefab, _myPosition, Quaternion.identity);
        _vfxInstance.Init(_projectileBehaviour.GetExplosionRadius());
    }
}
