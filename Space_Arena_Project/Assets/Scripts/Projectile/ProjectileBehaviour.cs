using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour
{
    [Title("// General")]
    [SerializeField] protected ProjectileSO _projectileSO = null;
    //[SerializeField] protected LayerMask _layerMask = default;
    //[SerializeField] protected Vector2 _minMaxTimeUntilDestroy = default;
    //[SerializeField] protected int _maxPierceCount = 1;

    [Title("// Debug - Projectile")]
    [SerializeField, ReadOnly] protected ShootModel _shootModel = null;
    [SerializeField, ReadOnly] protected float _timeUntilDestroy = 0f;
    [SerializeField, ReadOnly] protected float _destroyTimer = 0f;
    [SerializeField, ReadOnly] protected int _currentPierceCount = 0;

    public virtual void Init(ShootModel _newShootModel)
    {
        _shootModel = _newShootModel;
        SetDestroyTimer();
    }

    protected void CheckDestroyTime()
    {
        _destroyTimer += Time.fixedDeltaTime;

        if (_destroyTimer >= _timeUntilDestroy)
        {
            Destroy(gameObject);
        }
    }

    protected void SetDestroyTimer()
    {
        _timeUntilDestroy = Random.Range(_projectileSO.MinMaxTimeUntilDestroy.x, _projectileSO.MinMaxTimeUntilDestroy.y);
        _destroyTimer = 0f;
    }

    public bool HasHitSource(GameObject _gameobjectHit)
    {
        return _gameobjectHit == _shootModel.CharacterSource;
    }

    public void IncreasePierceCount()
    {
        _currentPierceCount++;
    }

    public bool HasReachedMaxPierceCount()
    {
        return _currentPierceCount >= _projectileSO.MaxPierceCount;
    }

    protected IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(_timeUntilDestroy);
        Destroy(gameObject);
    }
}
