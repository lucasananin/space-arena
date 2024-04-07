using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : ProjectileBehaviour
{
    [Title("// Physical")]
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] CircleCollider2D _dummyCircleCollider = null;
    [Space]
    [SerializeField] float _moveSpeed = 20f;
    [SerializeField] bool _destroyOnCollision = true;
    [SerializeField] int _maxPierceCount = 1;
    [SerializeField] float _timeUntilDestroy = 1f;

    [Title("// Vfx")]
    [SerializeField] ParticleSystem _hitVfx = null;
    [Space]
    [SerializeField, ReadOnly] List<Collider2D> _collidersHit = default;
    [SerializeField, ReadOnly] Vector3 _lastPosition = default;
    [SerializeField, ReadOnly] int _currentPierceCount = 0;
    [SerializeField, ReadOnly] float _destroyTimer = 0f;

    private RaycastHit2D[] _results = new RaycastHit2D[5];

    private void FixedUpdate()
    {
        _lastPosition = transform.position;

        _destroyTimer += Time.fixedDeltaTime;

        if (_destroyTimer >= _timeUntilDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        float _distance = Vector2.Distance(transform.position, _lastPosition);
        int _hits = Physics2D.CircleCastNonAlloc(_lastPosition, _dummyCircleCollider.radius, transform.right, _results, _distance, _layerMask);

        for (int i = 0; i < _hits; i++)
        {
            if (HasHitSource(_results[i].collider.gameObject)) continue;
            if (_collidersHit.Contains(_results[i].collider)) continue;

            _collidersHit.Add(_results[i].collider);
            Instantiate(_hitVfx, _results[i].point, Quaternion.identity);

            _currentPierceCount++;

            if (_currentPierceCount >= _maxPierceCount && _destroyOnCollision)
            {
                Destroy(gameObject);
                break;
            }
        }
    }

    public override void Init(ShootModel _shootModel)
    {
        this._shootModel = _shootModel;
        _lastPosition = transform.position;
        _rb.velocity = transform.right * _moveSpeed;
    }
}
