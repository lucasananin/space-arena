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

            var _damage = _shootModel.GetDamage();
            var _damageModel = new DamageModel(_shootModel.EntitySource, _raycastHit.point, _damage);
            _healthBehaviour?.TakeDamage(_damageModel);

            _collidersHit.Add(_colliderHit);

            if (_healthBehaviour is not null)
                IncreasePierceCount();

            SendRaycastHitEvent(_raycastHit);

            // Se atravessar obstaculos nao pode dar bounce.

            var _dot = Vector3.Dot(transform.right, _raycastHit.normal);
            bool _hasHitAnOppositeNormal = _dot < 0;
            bool _canBounce = true;
            if (_canBounce && HasHitObstacle(_colliderHit) && _hasHitAnOppositeNormal)
            {
                _collidersHit.Remove(_colliderHit);
                Reflect(_raycastHit);
                continue;
            }

            if (HasHitObstacle(_colliderHit))
            {
                _collidersHit.Remove(_colliderHit);
            }

            if (HasReachedMaxPierceCount() || (HasHitObstacle(_colliderHit) && !_canBounce))
            {
                DestroyThis();
                break;
            }
        }

        SetLastPosition();
        //Debug.Log($"// {transform.right}");
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

    //private bool IsNormalOnTheSameDirection(Vector3 _normal)
    //{
    //    var _myPosition = transform.position;
    //    var _right = transform.right;

    //    if (_right.x >= 0 && _right.y >= 0)
    //    {
    //        // right Up.
    //        return _normal.x >= 0 && _normal.y >= 0;
    //        //return _myPosition.x > _point.x;
    //    }
    //    else if (_right.x >= 0 && _right.y < 0)
    //    {
    //        // right Down.
    //        //return _myPosition.x > _point.x;
    //    }
    //    else if (_right.x < 0 && _right.y < 0)
    //    {
    //        // Left Down
    //        //return _myPosition.x < _point.x;
    //    }
    //    else if (_right.x < 0 && _right.y >= 0)
    //    {
    //        // Left Up
    //        //return _myPosition.x < _point.x;
    //    }

    //    return false;
    //}
}
