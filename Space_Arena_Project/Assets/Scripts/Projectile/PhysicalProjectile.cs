using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : ProjectileBehaviour
{
    [Title("// Physical")]
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] CircleCollider2D _dummyCircleCollider = null;

    [Title("// Vfx")]
    [SerializeField] ParticleSystem _hitVfx = null;

    [Title("// Debug - Physical")]
    [SerializeField, ReadOnly] List<Collider2D> _collidersHit = default;
    [SerializeField, ReadOnly] Vector3 _lastPosition = default;

    private RaycastHit2D[] _results = new RaycastHit2D[5];

    private void FixedUpdate()
    {
        _lastPosition = transform.position;
        CheckDestroyTime();
    }

    private void LateUpdate()
    {
        float _distance = Vector2.Distance(transform.position, _lastPosition);
        int _hits = Physics2D.CircleCastNonAlloc(_lastPosition, _dummyCircleCollider.radius, transform.right, _results, _distance, _projectileSO.LayerMask);

        for (int i = 0; i < _hits; i++)
        {
            if (HasHitSource(_results[i].collider.gameObject)) continue;
            if (_collidersHit.Contains(_results[i].collider)) continue;

            _collidersHit.Add(_results[i].collider);
            Instantiate(_hitVfx, _results[i].point, Quaternion.identity);

            IncreasePierceCount();

            if (HasReachedMaxPierceCount() && _projectileSO.DestroyOnCollision)
            {
                Destroy(gameObject);
                break;
            }
        }
    }

    public override void Init(ShootModel _newShootModel)
    {
        base.Init(_newShootModel);
        _lastPosition = transform.position;
        _rb.velocity = transform.right * _projectileSO.MoveSpeed;
    }
}
