using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehaviour : MonoBehaviour
{
    [SerializeField] EntitySO _entitySO = null;
    [SerializeField] HealthBehaviour _healthBehaviour = null;

    public T GetEntitySO<T>() where T : EntitySO
    {
        return _entitySO as T;
    }

    public bool IsAlive()
    {
        return _healthBehaviour.IsAlive();
    }

    public abstract bool IsMoving();
}
