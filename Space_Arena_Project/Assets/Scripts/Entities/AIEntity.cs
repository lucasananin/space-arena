using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntity : EntityBehaviour
{
    [SerializeField] AIPath _aiPath = null;
    [SerializeField, ReadOnly] EntityBehaviour _targetEntity = null;

    public void SetTargetEntity(GameObject _gameobject)
    {
        _targetEntity = _gameobject.GetComponent<EntityBehaviour>();
    }

    public bool HasTargetEntity()
    {
        return _targetEntity != null && _targetEntity.IsAlive();
    }

    public AIPath GetAIPath()
    {
        return _aiPath;
    }

    public Vector3 GetTargetEntityPosition()
    {
        return _targetEntity.transform.position;
    }
}
