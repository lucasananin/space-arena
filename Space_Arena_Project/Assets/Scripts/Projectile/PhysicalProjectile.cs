using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : ProjectileBehaviour
{
    [SerializeField] float _moveSpeed = 20f;
    [SerializeField] float _castRadius = 1f;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField, ReadOnly] Vector3 _lastPosition = default;
    [SerializeField, ReadOnly] ShootModel _shootModel = null;
    [Space]
    [SerializeField] ParticleSystem _hitVfx = null;

    private RaycastHit2D[] _results = new RaycastHit2D[5];

    private void Update()
    {
        _lastPosition = transform.position;
        transform.position += transform.right * _moveSpeed  * Time.deltaTime;
    }

    private void LateUpdate()
    {
        float _distance = Vector2.Distance(transform.position, _lastPosition);
        int _hits = Physics2D.CircleCastNonAlloc(_lastPosition, _castRadius, transform.right, _results, _distance, _layerMask);

        if (_hits > 0)
        {
            for (int i = 0; i < _hits; i++)
            {
                if (i > 0) break;
                if (_results[i].collider.gameObject == _shootModel.CharacterSource) return;

                Instantiate(_hitVfx, _results[i].point, Quaternion.identity);
            }

            //Debug.Log($"// _hits: {_hits}");
            Destroy(gameObject);
        }
    }

    public override void Init(ShootModel _shootModel)
    {
        this._shootModel = _shootModel;

        int _hits = Physics2D.CircleCastNonAlloc(-transform.right, _castRadius, transform.right, _results, 1f, _layerMask);

        if (_hits > 0)
        {
            for (int i = 0; i < _hits; i++)
            {
                if (_results[i].collider.gameObject == _shootModel.CharacterSource) return;

                Instantiate(_hitVfx, _results[i].point, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
