using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : ProjectileBehaviour
{
    [Title("// Physical")]
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] CircleCollider2D _dummyCircleCollider = null;

    [Title("// Debug - Physical")]
    [SerializeField, ReadOnly] List<Collider2D> _collidersHit = default;
    [SerializeField, ReadOnly] Vector3 _lastPosition = default;
    [SerializeField, ReadOnly] Vector2 _defaultVelocity = default;
    [SerializeField, ReadOnly] float _accelerationTime = 0f;

    private RaycastHit2D[] _results = new RaycastHit2D[9];

    private void FixedUpdate()
    {
        _lastPosition = transform.position;
        CheckAcceleration();
        CheckDestroyTime();
    }

    private void LateUpdate()
    {
        float _distance = Vector2.Distance(transform.position, _lastPosition);
        int _hits = Physics2D.CircleCastNonAlloc(_lastPosition, _dummyCircleCollider.radius, transform.right, _results, _distance, _projectileSO.LayerMask);

        for (int i = 0; i < _hits; i++)
        {
            RaycastHit2D _raycastHit = _results[i];
            Collider2D _colliderHit = _raycastHit.collider;

            if (HasHitSource(_colliderHit.gameObject)) continue;
            if (_collidersHit.Contains(_colliderHit)) continue;
            if (_colliderHit.TryGetComponent(out HealthBehaviour _healthBehaviour) && !HasAvailableTag(_colliderHit.gameObject)) continue;

            _healthBehaviour?.TakeDamage(1);
            _collidersHit.Add(_colliderHit);
            IncreasePierceCount();
            SendRaycastHitEvent(_raycastHit);

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
        _collidersHit.Clear();

        InitAccelerationParameters();
    }

    private void InitAccelerationParameters()
    {
        _defaultVelocity = _rb.velocity;
        _accelerationTime = 0f;
    }

    private void CheckAcceleration()
    {
        if (_projectileSO.UseAccelerationCurve)
        {
            _accelerationTime += Time.fixedDeltaTime * _projectileSO.AcelerationMultiplier;
            float _curveValue = _projectileSO.AccelerationCurve.Evaluate(_accelerationTime);

            Vector2 _newVelocity = _projectileSO.InvertAcceleration ?
                Vector2.Lerp(Vector2.zero, _defaultVelocity, _curveValue) :
                Vector2.Lerp(_defaultVelocity, Vector2.zero, _curveValue);

            _rb.velocity = _newVelocity;
        }
    }
}
