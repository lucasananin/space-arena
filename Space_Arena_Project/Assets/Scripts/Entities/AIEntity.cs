using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntity : EntityBehaviour
{
    [SerializeField] AIPath _aiPath = null;
    [SerializeField, ReadOnly] EntityBehaviour _targetEntity = null;

    //public void SetTargetEntity(GameObject _gameobject)
    //{
    //    _targetEntity = _gameobject.GetComponent<EntityBehaviour>();
    //}

    public void SetTargetEntity(EntityBehaviour _entityValue)
    {
        _targetEntity = _entityValue;
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

    public bool IsCloseToTargetEntity(float _minDistance)
    {
        float _distance = (GetTargetEntityPosition() - transform.position).sqrMagnitude;
        return _distance < _minDistance * _minDistance;
    }
}
