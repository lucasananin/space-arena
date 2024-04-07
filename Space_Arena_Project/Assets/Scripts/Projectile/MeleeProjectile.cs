using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeProjectile : ProjectileBehaviour
{
    [Title("// Melee")]
    [SerializeField] Rigidbody2D _rb = null;
    //[SerializeField] CircleCollider2D _dummyCircleCollider = null;
    [SerializeField] PolygonCollider2D _polygonCollider = null;
    [SerializeField] ContactFilter2D _contactFilter = default;
    [SerializeField] float _moveSpeed = 20f;
    [SerializeField] ParticleSystem _hitVfx = null;
    [SerializeField, ReadOnly] Vector3 _lastPosition = default;
    [SerializeField, ReadOnly] List<Collider2D> _collidersHit = default;

    [Space]
    //[SerializeField] SpriteRenderer _slashVfx = null;
    //[SerializeField] float _slashVfxTime = 0.2f;
    [SerializeField] float _timeToDestroy = 1f;
    [SerializeField] float _destroyTimer = 0;

    private RaycastHit2D[] _results = new RaycastHit2D[5];

    private void FixedUpdate()
    {
        _lastPosition = transform.position;

        _destroyTimer += Time.fixedDeltaTime;

        if (_destroyTimer >= _timeToDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        //return;
        float _distance = Vector2.Distance(transform.position, _lastPosition);
        //int _hits = Physics2D.CircleCastNonAlloc(_lastPosition, _dummyCircleCollider.radius, transform.right, _results, _distance, _layerMask);
        var _hits = _polygonCollider.Cast(transform.right, _contactFilter, _results, 0, true);
        //bool _canDestroy = false;

        for (int i = 0; i < _hits; i++)
        {
            if (i > 0) break;
            if (HasHitSource(_results[i].collider.gameObject)) continue;
            if (_collidersHit.Contains(_results[i].collider)) continue;
            // verificar se nao tem uma parede entre o source e o alvo.

            //_canDestroy = true;
            _collidersHit.Add(_results[i].collider);
            Instantiate(_hitVfx, _results[i].point, Quaternion.identity);
        }

        //if (_canDestroy)
        //{
        //    Destroy(gameObject);
        //}

        //StartCoroutine(DestroyRoutine());
    }

    public override void Init(ShootModel _shootModel)
    {
        this._shootModel = _shootModel;
        _lastPosition = transform.position;
        _rb.velocity = transform.right * _moveSpeed;
        //StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine()
    {
        //yield return new WaitForSeconds(_slashVfxTime);

        //_slashVfx.enabled = false;

        //yield return new WaitForSeconds(_timeToDestroy - _slashVfxTime);

        yield return new WaitForSeconds(_timeToDestroy);

        Destroy(gameObject);
    }
}
