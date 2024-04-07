using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapProjectile : ProjectileBehaviour
{
    [Title("// Overlap")]
    [SerializeField] Collider2D _collider2D = null;
    [SerializeField] ContactFilter2D _contactFilter2D = default;
    [SerializeField] float _timeUntilDestroy = 1f;

    [Title("// Vfx")]
    [SerializeField] ParticleSystem _hitVfx = null;

    private RaycastHit2D[] _results = new RaycastHit2D[5];

    public override void Init(ShootModel _shootModel)
    {
        this._shootModel = _shootModel;

        int _hits = _collider2D.Cast(transform.right, _contactFilter2D, _results, 0, true);

        for (int i = 0; i < _hits; i++)
        {
            if (HasHitSource(_results[i].collider.gameObject)) continue;
            // verificar se nao tem uma parede entre o source e o alvo.

            Instantiate(_hitVfx, _results[i].collider.ClosestPoint(_collider2D.transform.position), Quaternion.identity);
        }

        if (_timeUntilDestroy > 0)
        {
            StartCoroutine(DestroyRoutine());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(_timeUntilDestroy);

        Destroy(gameObject);
    }
}
