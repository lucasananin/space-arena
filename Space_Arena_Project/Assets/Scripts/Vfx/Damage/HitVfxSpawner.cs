using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVfxSpawner : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] ParticleSystem _hitVfx = null;

    private void OnValidate()
    {
        _projectileBehaviour = GetComponent<ProjectileBehaviour>();
    }

    private void OnEnable()
    {
        _projectileBehaviour.OnRaycastHit += SpawnVfx;
    }

    private void OnDisable()
    {
        _projectileBehaviour.OnRaycastHit -= SpawnVfx;
    }

    private void SpawnVfx(RaycastHit2D _hitInfo)
    {
        if (_hitInfo.collider.TryGetComponent(out TakeHitVfxSpawner _takeHitVfx))
        {
            _takeHitVfx.SpawnVfx(_hitInfo);
        }
        else
        {
            var _i = Instantiate(_hitVfx, _hitInfo.point, Quaternion.identity);
            _i.transform.right = _hitInfo.normal;
        }
    }
}
