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
    [SerializeField, ReadOnly] ShootModel _shootModel = null;

    private RaycastHit2D[] _results = new RaycastHit2D[9];
    private bool _canDamage = true;

    public event System.Action OnDestroyStart = null;

    private void Update()
    {
        if (!_canDamage) return;

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
            StartCoroutine(Destroy_routine());
        }
    }

    public void Init(ShootModel _shootModel)
    {
        this._shootModel = _shootModel;
    }

    private void CauseDamage()
    {
        var _hits = _collider2D.Cast(Vector2.one, _contactFilter2D, _results, 0f, true);

        for (int i = 0; i < _hits; i++)
        {
            RaycastHit2D _raycastHit = _results[i];
            Collider2D _colliderHit = _raycastHit.collider;

            if (_colliderHit.TryGetComponent(out HealthBehaviour _healthBehaviour) && !HasAvailableTag(_colliderHit.gameObject)) continue;

            var _damage = _shootModel.GetExplosiveDamage();
            var _damageModel = new DamageModel(_shootModel.EntitySource, transform.position, _damage);
            _healthBehaviour?.TakeDamage(_damageModel);
        }
    }

    private IEnumerator Destroy_routine()
    {
        _canDamage = false;
        OnDestroyStart?.Invoke();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private bool HasAvailableTag(GameObject _gameObjectHit)
    {
        return GeneralMethods.HasAvailableTag(_gameObjectHit, _tagCollectionSO.Tags);
    }
}
