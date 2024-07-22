using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnHitPMod : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] SimpleExplosionVfx _vfxPrefab = null;

    private void OnValidate()
    {
        _projectileBehaviour = GetComponent<ProjectileBehaviour>();
    }

    private void OnEnable()
    {
        _projectileBehaviour.OnRaycastHit += Explode;
    }

    private void OnDisable()
    {
        _projectileBehaviour.OnRaycastHit -= Explode;
    }

    private void Explode(RaycastHit2D _raycastHit)
    {
        _projectileBehaviour.Explode(_raycastHit.point);

        SimpleExplosionVfx _vfxInstance = Instantiate(_vfxPrefab, _raycastHit.point, Quaternion.identity);
        _vfxInstance.Init(_projectileBehaviour.GetExplosionRadius());
    }
}
