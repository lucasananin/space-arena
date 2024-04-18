using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastProjectile : ProjectileBehaviour
{
    [Title("// Cast")]
    [SerializeField] CircleCollider2D _dummyCircleCollider = null;

    [Title("// Vfx")]
    [SerializeField] ParticleSystem _hitVfx = null;
    [SerializeField] CastLineVfx _lineVfx = null;

    private RaycastHit2D[] _results = new RaycastHit2D[9];
    private List<Vector3> _pointsHit = new List<Vector3>();

    public override void Init(ShootModel _newShootModel)
    {
        base.Init(_newShootModel);
        _pointsHit.Clear();

        int _hits = Physics2D.CircleCastNonAlloc(transform.position, _dummyCircleCollider.radius, transform.right, _results, _projectileSO.MaxCastDistance, _projectileSO.LayerMask);

        for (int i = 0; i < _hits; i++)
        {
            RaycastHit2D _raycastHit = _results[i];
            Collider2D _colliderHit = _raycastHit.collider;

            if (HasHitSource(_colliderHit.gameObject)) continue;

            if (_colliderHit.TryGetComponent(out HealthBehaviour _healthBehaviour))
            {
                _healthBehaviour.TakeDamage(1);
            }

            _pointsHit.Add(_raycastHit.point);
            Instantiate(_hitVfx, _raycastHit.point, Quaternion.identity);
            IncreasePierceCount();
            SendRaycastHitEvent(_raycastHit);

            if (HasReachedMaxPierceCount())
            {
                break;
            }
        }

        TrySpawnLineVfx();
        StartCoroutine(DestroyRoutine());
    }

    private void TrySpawnLineVfx()
    {
        if (_lineVfx == null) return;

        var _instance = Instantiate(_lineVfx, transform.position, Quaternion.identity);

        if (_pointsHit.Count > 0)
        {
            _instance.Init(_pointsHit[_pointsHit.Count - 1]);
        }
        else
        {
            _instance.Init(transform.right.normalized * _projectileSO.MaxCastDistance);
        }
    }
}
