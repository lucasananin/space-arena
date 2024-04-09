using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] protected LayerMask _layerMask = default;
    [SerializeField, ReadOnly] protected ShootModel _shootModel = null;
    [Space]
    [SerializeField] protected Vector2 _minMaxTimeUntilDestroy = default;
    [SerializeField, ReadOnly] protected float _timeUntilDestroy = 0f;
    [SerializeField, ReadOnly] protected float _destroyTimer = 0f;

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
        _timeUntilDestroy = Random.Range(_minMaxTimeUntilDestroy.x, _minMaxTimeUntilDestroy.y);
        _destroyTimer = 0f;
    }

    public bool HasHitSource(GameObject _gameobjectHit)
    {
        return _gameobjectHit == _shootModel.CharacterSource;
    }

    protected IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(_timeUntilDestroy);
        Destroy(gameObject);
    }
}
