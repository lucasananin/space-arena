using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [Title("// General")]
    [SerializeField] Collider2D _collider2D = null;
    [SerializeField] TagCollectionSO _tagCollectionSO = null;
    [SerializeField] ContactFilter2D _contactFilter2D = default;

    [Title("// Time Properties")]
    [SerializeField, Range(0f, 9f)] float _damageRate = 1f;
    [SerializeField] bool _enableDestroyTimer = false;
    [SerializeField, Range(0f, 9f)] float _timeUntilDestroy = 4f;

    [Title("// Debug")]
    [SerializeField, ReadOnly] float _nextDamage = 0f;
    [SerializeField, ReadOnly] float _destroyTimer = 0f;

    private RaycastHit2D[] _results = new RaycastHit2D[9];

    private void Update()
    {
        _nextDamage += Time.deltaTime;

        if (_nextDamage > _damageRate)
        {
            _nextDamage = 0f;
            CauseDamage();
        }

        if (!_enableDestroyTimer) return;

        _destroyTimer += Time.deltaTime;

        if (_destroyTimer > _timeUntilDestroy)
        {
            _destroyTimer = 0f;
            Destroy(gameObject);
        }
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
