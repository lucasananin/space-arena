using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] Collider2D _collider2D = null;
    [SerializeField] TagCollectionSO _tagCollectionSO = null;
    [SerializeField, Range(0f, 9f)] float _fireRate = 1f;
    [SerializeField] ContactFilter2D _contactFilter2D = default;
    [SerializeField, ReadOnly] float _nextFire = 0f;

    private RaycastHit2D[] _results = new RaycastHit2D[9];

    private void Update()
    {
        _nextFire += Time.deltaTime;

        if (_nextFire > _fireRate)
        {
            _nextFire = 0f;
            CauseDamage();
        }

        // destroy by time.
    }

    private void CauseDamage()
    {
        var _hits = _collider2D.Cast(Vector2.one, _contactFilter2D, _results, 0f, true);

        for (int i = 0; i < _hits; i++)
        {
            RaycastHit2D _raycastHit = _results[i];
            Collider2D _colliderHit = _raycastHit.collider;

            if (_colliderHit.TryGetComponent(out HealthBehaviour _healthBehaviour) && !HasAvailableTag(_colliderHit.gameObject)) continue;

            _healthBehaviour?.TakeDamage(1);
        }
    }

    private bool HasAvailableTag(GameObject _gameObjectHit)
    {
        return GeneralMethods.HasAvailableTag(_gameObjectHit, _tagCollectionSO.Tags);
    }
}
