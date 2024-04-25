using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntity : EntityBehaviour
{
    [SerializeField, ReadOnly] EntityBehaviour _targetEntity = null;

    public void SetTargetEntity(GameObject _gameobject)
    {
        _targetEntity = _gameobject.GetComponent<EntityBehaviour>();
    }

    public bool HasTargetEntity()
    {
        return _targetEntity != null && _targetEntity.IsAlive();
    }
}
