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
    public AIPath AiPath { get => _aiPath; private set => _aiPath = value; }

    //public void SetTargetEntity(GameObject _gameobject)
    //{
    //    _targetEntity = _gameobject.GetComponent<EntityBehaviour>();
    //}

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

    public void SetTargetEntity(EntityBehaviour _entityValue)
    {
        _targetEntity = _entityValue;
    }

    public bool HasTargetEntity()
    {
        return _targetEntity != null && _targetEntity.IsAlive();
    }

    public Vector3 GetTargetEntityPosition()
    {
        return _targetEntity.transform.position;
    }

    public bool IsCloseToTargetEntity(float _minDistance)
    {
        return IsPointCloseToTargetEntity(transform.position, _minDistance);
    }

    public bool IsPointCloseToTargetEntity(Vector3 _point, float _minDistance)
    {
        float _distance = (GetTargetEntityPosition() - _point).sqrMagnitude;
        return _distance < _minDistance * _minDistance;
    }

    public bool HasReachedPathEnding()
    {
        return !_aiPath.pathPending && (_aiPath.reachedEndOfPath || !_aiPath.hasPath);
    }
}
