using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapProjectile : ProjectileBehaviour
{
    [Title("// Overlap")]
    [SerializeField] CapsuleCollider2D _dummyCapsuleCollider = null;
    [SerializeField] Collider2D _collider2D = null;
    [SerializeField] ParticleSystem _hitVfx = null;
    [SerializeField] ContactFilter2D _contactFilter2D = default;

    //private Collider2D[] _results = new Collider2D[5];
    private RaycastHit2D[] _results = new RaycastHit2D[5];

    public override void Init(ShootModel _shootModel)
    {
        this._shootModel = _shootModel;

        Vector3 _xOffset = transform.right * _dummyCapsuleCollider.offset.x;
        Vector3 _yOffset = transform.up * _dummyCapsuleCollider.offset.y;
        Vector3 _point = transform.position /*+ _xOffset + _yOffset*/;
        //int _hits = Physics2D.OverlapCapsuleNonAlloc(_point, _dummyCapsuleCollider.size /** 0.5f*/, _dummyCapsuleCollider.direction, transform.rotation.z, _results, _layerMask);
        int _hits = _collider2D.Cast(transform.right, _contactFilter2D, _results, 0, true);

        for (int i = 0; i < _hits; i++)
        {
            //if (HasHitSource(_results[i].gameObject)) continue;
            if (HasHitSource(_results[i].collider.gameObject)) continue;
            // verificar se nao tem uma parede entre o source e o alvo.

            Instantiate(_hitVfx, _results[i].collider.transform.position, Quaternion.identity);
            //Instantiate(_hitVfx, _results[i].collider.ClosestPoint(_collider2D.transform.position), Quaternion.identity);
        }

        //Destroy(gameObject);
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
