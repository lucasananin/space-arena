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

    [Title("// Vfx")]
    [SerializeField] ParticleSystem _hitVfx = null;
    [Space]
    [SerializeField, ReadOnly] List<Collider2D> _collidersHit = default;
    [SerializeField, ReadOnly] Vector3 _lastPosition = default;
    [SerializeField, ReadOnly] int _currentPierceCount = 0;

    private RaycastHit2D[] _results = new RaycastHit2D[5];

    private void FixedUpdate()
    {
        _lastPosition = transform.position;
        CheckDestroyTime();
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
        base.Init(_shootModel);
        _lastPosition = transform.position;
        _rb.velocity = transform.right * _moveSpeed;

        //var _sourceRb = _shootModel.CharacterSource.GetComponent<Rigidbody2D>();

        //_rb.velocity = _sourceRb.velocity;
        //_rb.AddForce(_shootModel.WeaponSource.transform.right * _moveSpeed, ForceMode2D.Impulse);

        //var _playerMover = _shootModel.CharacterSource.GetComponent<PlayerMover>();
        //var _sourceVelocityMagnitude = _playerMover.GetVelocityMagnitude();
        //_rb.velocity = (transform.right * _moveSpeed) + (transform.right * _sourceVelocityMagnitude);
        //Debug.Log($"// _sourceVelocityMagnitude = {_sourceVelocityMagnitude}");

        //float k = Vector2.Dot(transform.right, _sourceRb.velocity) / _sourceRb.velocity.magnitude;
        //Vector2 extraVelocity = k * transform.right * _sourceRb.velocity.magnitude;
        //_rb.velocity = extraVelocity;

        //Vector2 extraVelocity = Vector2.Dot(transform.right, _sourceRb.velocity) * transform.right;
        //_rb.velocity = extraVelocity;

        //var _a = 0.5f;
        //_rb.velocity = transform.rotation * Vector2.right * (_moveSpeed + Vector2.Dot(_sourceRb.velocity, _sourceRb.rotation * Vector2.one) / _a);
    }
}
