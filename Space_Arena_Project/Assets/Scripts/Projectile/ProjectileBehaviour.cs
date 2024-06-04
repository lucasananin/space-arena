using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour
{
    [Title("// General")]
    [SerializeField] protected ProjectileSO _projectileSO = null;

    [Title("// Debug - Projectile")]
    [SerializeField, ReadOnly] protected ShootModel _shootModel = null;
    [SerializeField, ReadOnly] protected float _timeUntilDestroy = 0f;
    [SerializeField, ReadOnly] protected float _destroyTimer = 0f;
    [SerializeField, ReadOnly] protected int _currentPierceCount = 0;

    private Collider2D[] _explosionResults = new Collider2D[9];
    private List<EntityBehaviour> _entitiesFound = new List<EntityBehaviour>();

    public event System.Action<RaycastHit2D> onRaycastHit = null;
    public event System.Action OnDestroy = null;
    public event System.Action OnDestroyTimerEnd = null;

    public virtual void Init(ShootModel _newShootModel)
    {
        _shootModel = _newShootModel;
        SetDestroyTimer();
        TryAutoRotate();
    }

    protected void SendRaycastHitEvent(RaycastHit2D _value)
    {
        onRaycastHit?.Invoke(_value);
    }

    protected void CheckDestroyTime()
    {
        _destroyTimer += Time.fixedDeltaTime;

        if (_destroyTimer >= _timeUntilDestroy)
        {
            DestroyByTime();
        }
    }

    protected void SetDestroyTimer()
    {
        _timeUntilDestroy = Random.Range(_projectileSO.MinMaxTimeUntilDestroy.x, _projectileSO.MinMaxTimeUntilDestroy.y);
        _destroyTimer = 0f;
    }

    public void Explode()
    {
        Explode(transform.position);
    }

    public void Explode(Vector3 _point)
    {
        int _hits = Physics2D.OverlapCircleNonAlloc(_point, _projectileSO.ExplosionRadius, _explosionResults, _projectileSO.LayerMask);

        for (int i = 0; i < _hits; i++)
        {
            if (_explosionResults[i].TryGetComponent(out HealthBehaviour _healthBehaviour))
            {
                _healthBehaviour.TakeDamage(1);
            }
        }
    }

    private void TryAutoRotate()
    {
        if (!_projectileSO.CanAutoRotate) return;

        _entitiesFound.Clear();
        var _myPosition = transform.position;
        var _hits = Physics2D.OverlapCircleNonAlloc(_myPosition, 5, _explosionResults, _projectileSO.EntityLayerMask);

        for (int i = 0; i < _hits; i++)
        {
            var _colliderHit = _explosionResults[i];

            if (HasHitSource(_colliderHit.gameObject)) continue;

            if (_colliderHit.TryGetComponent(out EntityBehaviour _entity))
            {
                _entitiesFound.Add(_entity);
            }
        }

        _entitiesFound = GeneralMethods.OrderListByDistance(_entitiesFound, _myPosition);

        int _count = _entitiesFound.Count;

        for (int i = 0; i < _count; i++)
        {
            var _entityPosition = _entitiesFound[i].transform.position;
            var _angle = GeneralMethods.CalculateAngle(_entityPosition, transform);

            if (_angle < _projectileSO.MaxAngle / 2f)
            {
                transform.rotation = GeneralMethods.GetLookRotation(_myPosition, _entityPosition);
                break;
            }
        }
    }

    public float GetExplosionRadius()
    {
        return _projectileSO.ExplosionRadius;
    }

    public void IncreasePierceCount()
    {
        _currentPierceCount++;
    }

    public bool HasReachedMaxPierceCount()
    {
        return _currentPierceCount >= _projectileSO.MaxPierceCount;
    }

    public bool HasHitSource(GameObject _gameobjectHit)
    {
        return _gameobjectHit == _shootModel.EntitySource.gameObject;
    }

    public bool HasAvailableTag(GameObject _gameObjectHit)
    {
        return GeneralMethods.HasAvailableTag(_gameObjectHit, _projectileSO.Tags);
    }

    protected IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(_timeUntilDestroy);
        DestroyByTime();
    }

    public void DestroyByTime()
    {
        OnDestroyTimerEnd?.Invoke();
        DestroyThis();
    }

    public void DestroyThis()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }
}
