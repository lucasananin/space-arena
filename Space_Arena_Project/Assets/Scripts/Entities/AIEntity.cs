using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEntity : EntityBehaviour
{
    [SerializeField] AIPath _aiPath = null;
    [SerializeField] TagCollectionSO _obstacleTags = null;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField, ReadOnly] EntityBehaviour _targetEntity = null;
    [SerializeField, ReadOnly] bool _isFleeing = false;
    [SerializeField, ReadOnly] bool _isCowering = false;
    [SerializeField, ReadOnly] bool _isTargetOnLineOfSight = false;
    [SerializeField, ReadOnly] float _timeUntilSearchPath = 0f;
    [SerializeField, ReadOnly] float _searchPathTimer = 0f;

    private RaycastHit2D[] _results = new RaycastHit2D[9];

    public AIPath AiPath { get => _aiPath; private set => _aiPath = value; }
    public bool IsFleeing { get => _isFleeing; set => _isFleeing = value; }
    public bool IsCowering { get => _isCowering; set => _isCowering = value; }
    public bool IsTargetOnLineOfSight { get => _isTargetOnLineOfSight; private set => _isTargetOnLineOfSight = value; }

    private void Update()
    {
        _isTargetOnLineOfSight = HasTargetEntity() && CanSeeTargetFromPoint(transform.position);
    }

    public void SetTargetEntity(EntityBehaviour _entityValue)
    {
        _targetEntity = _entityValue;
    }

    public bool IsTargetEntity(GameObject _obj)
    {
        return _obj == _targetEntity.gameObject;
    }

    public bool HasTargetEntity()
    {
        return _targetEntity != null && _targetEntity.IsAlive();
    }

    public Vector3 GetTargetEntityPosition()
    {
        return _targetEntity.transform.position;
    }

    //public Vector3 PickRandomPointAround(float _radius)
    //{
    //    Vector3 _point = Random.insideUnitCircle * _radius;
    //    _point += _aiPath.position;
    //    return _point;
    //}

    public Vector3 PickRandomPointAround(Vector2 _range)
    {
        Vector3 _point = GeneralMethods.GetRandomInCircle(_range.x, _range.y);
        _point += _aiPath.position;
        return _point;
    }

    public Vector3 PickRandomPointNearTarget(Vector2 _minMaxValue)
    {
        Vector3 _point = GeneralMethods.GetRandomInCircle(_minMaxValue.x, _minMaxValue.y);
        _point += GetTargetEntityPosition();
        return _point;
    }

    public Vector3 PickTargetFlank(Vector3 _range, float _distance)
    {
        var _direction = (transform.position - GetTargetEntityPosition()).normalized;
        var _cross = Vector3.Cross(_direction, transform.forward);
        _cross *= Random.Range(0, 2) == 0 ? 1f : -1f;

        Vector3 _point = GeneralMethods.GetRandomInCircle(_range.x, _range.y);
        _point += GetTargetEntityPosition();
        _point += _cross * _distance;
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
        return GeneralMethods.IsPointCloseToTarget(_point, GetTargetEntityPosition(), _minDistance);
    }

    public void SetAIPathDestination(Vector3 _position)
    {
        _aiPath.destination = _position;
        _aiPath.SearchPath();
    }

    public void StopAiPath()
    {
        if (!IsMoving()) return;
        SetAIPathDestination(transform.position);
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
        int _hits = Physics2D.CircleCastNonAlloc(_point, 0.3f, _direction, _results, _distance, _layerMask);

        for (int i = 0; i < _hits; i++)
        {
            Collider2D _colliderHit = _results[i].collider;

            if (GeneralMethods.HasAvailableTag(_colliderHit.gameObject, _obstacleTags.Tags)) return false;

            if (IsTargetEntity(_colliderHit.gameObject))
            {
                return true;
            }
        }

        return false;
    }

    public bool IsWaitingToSearchPath()
    {
        _searchPathTimer += Time.deltaTime;
        return _searchPathTimer < _timeUntilSearchPath;
    }

    public void ResetTimeUntilSearchPath()
    {
        Vector2 _minMaxValue = GetEntitySO<AiEntitySO>().MoveRateRange;
        _timeUntilSearchPath = Random.Range(_minMaxValue.x, _minMaxValue.y);
        _searchPathTimer = 0;
    }

    public override bool IsMoving()
    {
        return _aiPath.velocity != Vector3.zero;
    }
}
