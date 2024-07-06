using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_BullCharge", menuName = "SO/State Machines/Actions/AI Bull Charge")]
public class AiBullChargeActionSO : StateActionSO<AiBullChargeAction>
{
    [SerializeField] LayerMask _obstacleLayerMask = default;

    public LayerMask ObstacleLayerMask { get => _obstacleLayerMask; private set => _obstacleLayerMask = value; }
}

public class AiBullChargeAction : StateAction
{
    private new AiBullChargeActionSO OriginSO => (AiBullChargeActionSO)base.OriginSO;

    private AiEntity _aiEntity = null;
    private AiEntitySO _entitySO = null;
    private AIPath _aiPath = null;
    private RaycastHit2D[] _results = new RaycastHit2D[2];
    private float _timer = 0f;
    private float _waitTime = 0f;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _entitySO = _aiEntity.GetEntitySO<AiEntitySO>();
        _aiPath = _stateMachine.GetComponent<AIPath>();
    }

    public override void OnStateEnter()
    {
        _timer = 0;
        _waitTime = Random.Range(_entitySO.ChargingWaitRange.x, _entitySO.ChargingWaitRange.y);
        _aiEntity.IsWaitingBullCharge = true;
        _aiEntity.IsBullCharging = false;
        _aiEntity.StopAiPath();
        _aiPath.maxSpeed *= _entitySO.ChargingSpeedMultiplier;
    }

    public override void OnStateExit()
    {
        _aiPath.maxSpeed /= _entitySO.ChargingSpeedMultiplier;
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > _waitTime && _aiEntity.IsWaitingBullCharge)
        {
            _aiEntity.IsWaitingBullCharge = false;
            _aiEntity.IsBullCharging = true;
            SearchPath();
        }

        if (_aiEntity.HasReachedPathEnding() && !_aiEntity.IsWaitingBullCharge)
        {
            _aiEntity.IsBullCharging = false;
        }
    }

    private void SearchPath()
    {
        var _point = GetPoint();
        _aiEntity.SetAIPathDestination(_point);
        _aiEntity.ResetTimeUntilSearchPath();
    }

    private Vector3 GetPoint()
    {
        var _targetPosition = _aiEntity.GetTargetEntityPosition();
        var _direction = (_targetPosition - _aiEntity.transform.position).normalized;
        var _hits = Physics2D.RaycastNonAlloc(_targetPosition, _direction, _results, _entitySO.ChargingDistance, OriginSO.ObstacleLayerMask);

        for (int i = 0; i < _hits; i++)
        {
            return _results[0].point;
        }

        return _targetPosition + _direction * _entitySO.ChargingDistance;
    }
}
