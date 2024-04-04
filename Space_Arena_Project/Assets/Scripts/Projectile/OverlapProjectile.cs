using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapProjectile : ProjectileBehaviour
{
    [SerializeField] CapsuleCollider2D _dummyCapsuleCollider = null;
    [SerializeField] ParticleSystem _hitVfx = null;

    private Collider2D[] _results = new Collider2D[5];

    public override void Init(ShootModel _shootModel)
    {
        this._shootModel = _shootModel;

        Vector3 _xOffset = transform.right * _dummyCapsuleCollider.offset.x;
        Vector3 _yOffset = transform.up * _dummyCapsuleCollider.offset.y;
        Vector3 _point = transform.position + _xOffset + _yOffset;
        int _hits = Physics2D.OverlapCapsuleNonAlloc(_point, _dummyCapsuleCollider.size, _dummyCapsuleCollider.direction, 0, _results, _layerMask);

        if (_hits > 0)
        {
            for (int i = 0; i < _hits; i++)
            {
                if (_results[i].gameObject.gameObject == _shootModel.CharacterSource) return;
                // verificar se nao tem uma parede na frente do alvo.

                Instantiate(_hitVfx, _results[i].ClosestPoint(transform.position), Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}
