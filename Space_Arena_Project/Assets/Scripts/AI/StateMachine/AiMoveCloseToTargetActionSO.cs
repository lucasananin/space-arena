using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_MoveCloserToTarget", menuName = "SO/State Machines/Actions/AI Move Closer To Target")]
public class AiMoveCloseToTargetActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new AiMoveCloseToTargetAction();
    }
}

public class AiMoveCloseToTargetAction : StateAction
{
    private AiEntity _aiEntity = null;
    private AiEntitySO _entitySO = null;
    private Vector3 _point = default;

    public override void Awake(StateMachine stateMachine)
    {
        _aiEntity = stateMachine.GetComponent<AiEntity>();
        _entitySO = _aiEntity.GetEntitySO<AiEntitySO>();
    }

    public override void OnStateEnter()
    {
        SearchPath();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        if (_aiEntity.IsWaitingToSearchPath()) return;
        if (_entitySO.StopMovingOnClose && _aiEntity.IsCloseToTargetEntity(_entitySO.MoveCloseRange.y)) return;

        bool _isTargetFarFromPoint = /*_entitySO.RepathOnTargetFarAway && */!_aiEntity.IsPointCloseToTargetEntity(_point, _entitySO.MoveCloseRange.y);

        if (_aiEntity.HasReachedPathEnding() || _isTargetFarFromPoint)
        {
            SearchPath();
        }
    }

    private void SearchPath()
    {
        _point = TryGetPositionWhereTargetIsVisible();
        _aiEntity.SetAIPathDestination(_point);
        _aiEntity.ResetTimeUntilSearchPath();
    }

    private Vector3 TryGetPositionWhereTargetIsVisible()
    {
        Vector3 _point = _aiEntity.PickRandomPointNearTarget(_entitySO.MoveCloseRange);

        for (int i = 0; i < _entitySO.MaxNumberOfTries; i++)
        {
            if (_aiEntity.CanSeeTargetFromPoint(_point))
            {
                return _point;
            }
            else
            {
                _point = _aiEntity.PickRandomPointNearTarget(_entitySO.MoveCloseRange);
            }
        }

        return _point;
    }
}