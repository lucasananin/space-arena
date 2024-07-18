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
    [SerializeField, ReadOnly] float _speedMultiplier = 0f;

    private readonly RaycastHit2D[] _results = new RaycastHit2D[9];

    private void FixedUpdate()
    {
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

            if (HasHitObstacle(_colliderHit))
            {
                if (_stats.CanBounce)
                {
                    TryBounce(_raycastHit);
                }
                else
                {
                    _collidersHit.Add(_colliderHit);
                    TryDamage(_healthBehaviour, _raycastHit);
                    SendRaycastHitEvent(_raycastHit);

                    if (!_stats.CanPierceObstacles)
                    {
                        DestroyThis();
                        break;
                    }
                }
            }
            else
            {
                _collidersHit.Add(_colliderHit);
                TryDamage(_healthBehaviour, _raycastHit);
                SendRaycastHitEvent(_raycastHit);
                IncreasePierceCount();

                if (HasReachedMaxPierceCount())
                {
                    DestroyThis();
                    break;
                }
            }
        }

        SetLastPosition();
    }

    public override void Init(ShootModel _newShootModel)
    {
        base.Init(_newShootModel);
        _lastPosition = transform.position;
        _speedMultiplier = Random.Range(_stats.MoveSpeedRange.x, _stats.MoveSpeedRange.y);
        _rb.velocity = transform.right * _speedMultiplier;
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
        if (_stats.UseAccelerationCurve)
        {
            _accelerationTime += Time.fixedDeltaTime * _stats.AcelerationMultiplier;
            float _curveValue = _stats.AccelerationCurve.Evaluate(_accelerationTime);

            Vector2 _newVelocity = _stats.InvertAcceleration ?
                Vector2.Lerp(Vector2.zero, _defaultVelocity, _curveValue) :
                Vector2.Lerp(_defaultVelocity, Vector2.zero, _curveValue);

            _rb.velocity = _newVelocity;
        }

        if (_rb.velocity == Vector2.zero && _stats.DestroyOnStop)
        {
            DestroyByStop();
        }
    }

    private void TryDamage(HealthBehaviour _healthBehaviour, RaycastHit2D _raycastHit)
    {
        var _damage = _shootModel.GetDamage();
        var _damageModel = new DamageModel(_shootModel.EntitySource, _raycastHit.point, _damage);
        _healthBehaviour?.TakeDamage(_damageModel);
    }

    private void TryBounce(RaycastHit2D _raycastHit)
    {
        var _dot = Vector3.Dot(transform.right, _raycastHit.normal);
        var _hasHitAnOppositeNormal = _dot < 0;

        if (_hasHitAnOppositeNormal)
        {
            SendRaycastHitEvent(_raycastHit);
            Reflect(_raycastHit);
        }
    }

    private void Reflect(RaycastHit2D _raycastHit)
    {
        var _reflect = Vector2.Reflect(transform.right, _raycastHit.normal);
        transform.right = _reflect;
        _rb.velocity = transform.right * _speedMultiplier;
        _defaultVelocity = _rb.velocity;
    }

    private void SetLastPosition()
    {
        _lastPosition = transform.position;
    }
}
