using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : ProjectileBehaviour
{
    [Title("// Physical Properties")]
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] CircleCollider2D _dummyCircleCollider = null;
    [SerializeField] float _moveSpeed = 20f;
    [SerializeField] ParticleSystem _hitVfx = null;
    [SerializeField, ReadOnly] Vector3 _lastPosition = default;

    private RaycastHit2D[] _results = new RaycastHit2D[5];

    private void FixedUpdate()
    {
        _lastPosition = transform.position;
    }

    private void LateUpdate()
    {
        float _distance = Vector2.Distance(transform.position, _lastPosition);
        int _hits = Physics2D.CircleCastNonAlloc(_lastPosition, _dummyCircleCollider.radius, transform.right, _results, _distance, _layerMask);
        bool _canDestroy = false;

        for (int i = 0; i < _hits; i++)
        {
            if (i > 0) break;
            if (HasHitSource(_results[i].collider.gameObject)) continue;

            _canDestroy = true;
            Instantiate(_hitVfx, _results[i].point, Quaternion.identity);
        }

        if (_canDestroy)
        {
            Destroy(gameObject);
        }
    }

    public override void Init(ShootModel _shootModel)
    {
        this._shootModel = _shootModel;
        _lastPosition = transform.position;
        _rb.velocity = transform.right * _moveSpeed;
    }
}
