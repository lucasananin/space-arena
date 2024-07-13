using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour
{
    [Title("// Debug - Projectile")]
    [SerializeField, ReadOnly] protected ShootModel _shootModel = null;
    [SerializeField, ReadOnly] protected ProjectileSO _projectileSO = null;
    [SerializeField, ReadOnly] protected float _timeUntilDestroy = 0f;
    [SerializeField, ReadOnly] protected float _destroyTimer = 0f;
    [SerializeField, ReadOnly] protected int _currentPierceCount = 0;

    private Collider2D[] _explosionResults = new Collider2D[9];
    private RaycastHit2D[] _obstacleResults = new RaycastHit2D[9];
    private List<EntityBehaviour> _entitiesFound = new List<EntityBehaviour>();

    public event System.Action<ShootModel> OnInit = null;
    public event System.Action<RaycastHit2D> onRaycastHit = null;
    public event System.Action OnDestroy_TimerEnd = null;
    public event System.Action OnDestroy_Stop = null;
    public event System.Action OnExplode = null;

    public float TimeUntilDestroy { get => _timeUntilDestroy; }
    public ShootModel ShootModel { get => _shootModel; }

    public virtual void Init(ShootModel _newShootModel)
    {
        _shootModel = _newShootModel;
        _projectileSO = _newShootModel.ProjectileSO;
        SetDestroyTimer();
        TryAutoRotate();
    }

    protected void SendInitEvent()
    {
        OnInit?.Invoke(_shootModel);
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
            var _colliderHit = _explosionResults[i];

            if (_colliderHit.TryGetComponent(out HealthBehaviour _healthBehaviour))
            {
                if (HasObstacleBetween(_point, _colliderHit.transform.position)) continue;

                var _damage = _shootModel.GetExplosiveDamage();
                var _damageModel = new DamageModel(_shootModel.EntitySource, transform.position, _damage);
                _healthBehaviour.TakeDamage(_damageModel);
            }
        }

        OnExplode?.Invoke();
    }

    private bool HasObstacleBetween(Vector3 _origin, Vector3 _targetPoint)
    {
        var _vector = _targetPoint - _origin;
        var _rayHitCount = Physics2D.RaycastNonAlloc(_origin, _vector.normalized, _obstacleResults, _vector.magnitude, _projectileSO.ObstacleLayerMask);
        bool _hitObstacle = _rayHitCount > 0;
        return _hitObstacle;
    }

    private void TryAutoRotate()
    {
        var _so = _projectileSO;

        if (!_so.CanAutoRotate) return;

        _entitiesFound.Clear();
        var _myPosition = transform.position;
        var _hits = Physics2D.OverlapCircleNonAlloc(_myPosition, _so.MaxDistance_autoAim, _explosionResults, _so.EntityLayerMask);

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

            if (_angle < _so.MaxAngle / 2f)
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
        // organizar e otimizar esse codigo.
        if (_projectileSO.CanDamageProjectiles)
        {
            var _t = new string[] { "Projectile" };
            var _tags = _shootModel.EntitySource.GetProjectileHitTags();
            return GeneralMethods.HasAvailableTag(_gameObjectHit, _t) || GeneralMethods.HasAvailableTag(_gameObjectHit, _tags);
        }
        else
        {
            var _tags = _shootModel.EntitySource.GetProjectileHitTags();
            return GeneralMethods.HasAvailableTag(_gameObjectHit, _tags);
        }
    }

    public float GetExplodeTimeNormalized()
    {
        return Mathf.InverseLerp(0, _timeUntilDestroy, _destroyTimer);
    }

    protected IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(_timeUntilDestroy);
        DestroyByTime();
    }

    public void DestroyByTime()
    {
        OnDestroy_TimerEnd?.Invoke();
        DestroyThis();
    }

    public void DestroyByStop()
    {
        OnDestroy_Stop?.Invoke();
        DestroyThis();
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
