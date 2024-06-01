using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

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

    public event System.Action<RaycastHit2D> onRaycastHit = null;
    public event System.Action OnDestroy = null;
    public event System.Action OnDestroyTimerEnd = null;

    public virtual void Init(ShootModel _newShootModel)
    {
        _shootModel = _newShootModel;
        SetDestroyTimer();

        // da um overlap para pegar os inimigos ao redor.
        var _hits = Physics2D.OverlapCircleNonAlloc(transform.position, 5, _explosionResults, LayerMask.NameToLayer("Entity"));

        // faz uma lista ordenada pelo mais proximo.
        var _list = new List<Transform>();
        _list.Sort(delegate (Transform _a, Transform _b)
        {
            //return Vector2.Distance(transform.position, _a.transform.position).CompareTo(Vector2.Distance(transform.position, _b.transform.position));
            return (_a.position - transform.position).sqrMagnitude.CompareTo((_b.position - transform.position).sqrMagnitude);
        });

        // ve qual dos mais proximos esta dentro de um angulo x.
        // rotaciona em direcao a esse inimigo.
    }

    private void Fodase(List<Transform> _list)
    {
        //_list.Sort(delegate (Transform _a, Transform _b)
        //{
        //    return Vector2.Distance(transform.position, _a.transform.position).
        //    CompareTo(Vector2.Distance(transform.position, _b.transform.position));
        //});

        _list.Sort(delegate (Transform _a, Transform _b)
        {
            //return Vector2.Distance(transform.position, _a.transform.position).CompareTo(Vector2.Distance(transform.position, _b.transform.position));
            return (_a.position - transform.position).sqrMagnitude.CompareTo((_b.position - transform.position).sqrMagnitude);
        });
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

    //public void SetProjectileSO(ProjectileSO _value)
    //{
    //    _projectileSO = _value;
    //}
}
