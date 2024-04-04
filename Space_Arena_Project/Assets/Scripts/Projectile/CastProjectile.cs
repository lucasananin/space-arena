using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastProjectile : ProjectileBehaviour
{
    [Title("// Cast Properties")]
    [SerializeField] CircleCollider2D _dummyCircleCollider = null;
    [SerializeField] ParticleSystem _hitVfx = null;

    private RaycastHit2D[] _results = new RaycastHit2D[5];

    public const float CAST_MAX_DISTANCE = 99F;

    public override void Init(ShootModel _shootModel)
    {
        this._shootModel = _shootModel;

        int _hits = Physics2D.CircleCastNonAlloc(transform.position, _dummyCircleCollider.radius, transform.right, _results, CAST_MAX_DISTANCE, _layerMask);

        if (_hits > 0)
        {
            for (int i = 0; i < _hits; i++)
            {
                if (i > 0) break;
                if (_results[i].collider.gameObject == _shootModel.CharacterSource) return;

                Instantiate(_hitVfx, _results[i].point, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}
