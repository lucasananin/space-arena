using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastHitVfxSpawner : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] ParticleSystem _hitVfx = null;

    private void OnValidate()
    {
        _projectileBehaviour = GetComponent<ProjectileBehaviour>();
    }

    private void OnEnable()
    {
        _projectileBehaviour.onRaycastHit += SpawnVfx;
    }

    private void OnDisable()
    {
        _projectileBehaviour.onRaycastHit -= SpawnVfx;
    }

    private void SpawnVfx(RaycastHit2D _hitInfo)
    {
        Instantiate(_hitVfx, _hitInfo.point, Quaternion.identity);
    }
}
