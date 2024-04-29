using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEntity : EntityBehaviour
{
    [SerializeField] AIPath _aiPath = null;
    [SerializeField] LayerMask _obstacleLayerMask = default;
    [SerializeField, ReadOnly] EntityBehaviour _targetEntity = null;
    [SerializeField, ReadOnly] bool _isFleeing = false;
    [SerializeField, ReadOnly] bool _isTargetOnLineOfSight = false;

    private RaycastHit2D[] _results = new RaycastHit2D[9];

    public bool IsFleeing { get => _isFleeing; set => _isFleeing = value; }
    public AIPath AiPath { get => _aiPath; private set => _aiPath = value; }
    public bool IsTargetOnLineOfSight { get => _isTargetOnLineOfSight; private set => _isTargetOnLineOfSight = value; }

    private void Update()
    {
        _isTargetOnLineOfSight = HasTargetEntity() && CanSeeTargetFromPoint(transform.position);
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

    public Vector3 PickRandomPointAround(float _radius)
    {
        Vector3 _point = Random.insideUnitCircle * _radius;
        _point += _aiPath.position;
        return _point;
    }

    public Vector3 PickRandomPointNearTarget(float _radius)
    {
        Vector3 _point = Random.insideUnitCircle * _radius;
        _point += GetTargetEntityPosition();
        return _point;
    }

    public Vector3 PickRandomPointAwayFromTarget(float _radius, float _distance)
    {
        Vector3 _point = Random.insideUnitCircle * _radius;
        _point += GetTargetEntityPosition();
        _point += (transform.position - GetTargetEntityPosition()).normalized * _distance;
        return _point;
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

    public void SetAIPathDestination(Vector3 _position)
    {
        _aiPath.destination = _position;
        _aiPath.SearchPath();
    }

    public bool HasReachedPathEnding()
    {
        return !_aiPath.pathPending && (_aiPath.reachedEndOfPath || !_aiPath.hasPath);
    }

    public bool CanSeeTargetFromPoint(Vector3 _point)
    {
        Vector3 _vector = GetTargetEntityPosition() - _point;
        Vector3 _direction = _vector.normalized;
        float _distance = _vector.magnitude;
        int _hits = Physics2D.CircleCastNonAlloc(_point, 0.3f, _direction, _results, _distance, _obstacleLayerMask);
        return _hits <= 0;
    }
}
