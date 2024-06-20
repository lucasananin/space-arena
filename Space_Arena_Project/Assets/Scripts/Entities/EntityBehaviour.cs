using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehaviour : MonoBehaviour
{
    [SerializeField] protected EntitySO _entitySO = null;
    [SerializeField] protected HealthBehaviour _healthBehaviour = null;

    public T GetEntitySO<T>() where T : EntitySO
    {
        return _entitySO as T;
    }

    public bool IsAlive()
    {
        return _healthBehaviour.IsAlive();
    }

    public string[] GetOpponentTags()
    {
        return _entitySO.OpponentTags.Tags;
    }

    public string[] GetProjectileHitTags()
    {
        return _entitySO.ProjectileHitTags.Tags;
    }

    public abstract bool IsMoving();
}
