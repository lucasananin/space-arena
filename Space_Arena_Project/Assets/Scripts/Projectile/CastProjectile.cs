using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastProjectile : ProjectileBehaviour
{
    [Title("// Cast")]
    [SerializeField] CircleCollider2D _dummyCircleCollider = null;

    private RaycastHit2D[] _results = new RaycastHit2D[9];
    private List<RaycastHit2D> _raycastHits = new List<RaycastHit2D>();

    public event System.Action<List<RaycastHit2D>> onCastEnd = null;

    public override void Init(ShootModel _newShootModel)
    {
        base.Init(_newShootModel);
        _raycastHits.Clear();

        int _hits = Physics2D.CircleCastNonAlloc(transform.position, _dummyCircleCollider.radius, transform.right, _results, _projectileSO.MaxCastDistance, _projectileSO.LayerMask);

        for (int i = 0; i < _hits; i++)
        {
            RaycastHit2D _raycastHit = _results[i];
            Collider2D _colliderHit = _raycastHit.collider;

            if (HasHitSource(_colliderHit.gameObject)) continue;
            if (_colliderHit.TryGetComponent(out HealthBehaviour _healthBehaviour) && !HasAvailableTag(_colliderHit.gameObject)) continue;

            _healthBehaviour?.TakeDamage(1);
            _raycastHits.Add(_raycastHit);
            IncreasePierceCount();
            SendRaycastHitEvent(_raycastHit);

            if (HasReachedMaxPierceCount())
            {
                break;
            }
        }

        onCastEnd?.Invoke(_raycastHits);
        StartCoroutine(DestroyRoutine());
    }

    public float GetCastMaxDistance()
    {
        return _projectileSO.MaxCastDistance;
    }
}
