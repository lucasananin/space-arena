using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastProjectile : ProjectileBehaviour
{
    [Title("// Cast")]
    [SerializeField] CircleCollider2D _dummyCircleCollider = null;
    [SerializeField] ParticleSystem _hitVfx = null;

    private RaycastHit2D[] _results = new RaycastHit2D[5];

    public const float CAST_MAX_DISTANCE = 99F;

    public override void Init(ShootModel _shootModel)
    {
        this._shootModel = _shootModel;

        int _hits = Physics2D.CircleCastNonAlloc(transform.position, _dummyCircleCollider.radius, transform.right, _results, CAST_MAX_DISTANCE, _layerMask);
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
}
