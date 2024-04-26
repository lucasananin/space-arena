using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntity : EntityBehaviour
{
    [SerializeField] AIPath _aiPath = null;
    [SerializeField] AIWeaponHandler _aIWeaponHandler = null;
    [SerializeField, ReadOnly] EntityBehaviour _targetEntity = null;
    [SerializeField, ReadOnly] bool _isFleeing = false;

    public bool IsFleeing { get => _isFleeing; set => _isFleeing = value; }

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

    public void PullTrigger()
    {
        _aIWeaponHandler.PullTrigger();
    }

    public void RotateWeaponToTarget()
    {
        if (HasTargetEntity())
        {
            _aIWeaponHandler.RotateWeapon(GetTargetEntityPosition());
        }
        else
        {
            _aIWeaponHandler.ResetWeaponRotation();
        }
    }
}
