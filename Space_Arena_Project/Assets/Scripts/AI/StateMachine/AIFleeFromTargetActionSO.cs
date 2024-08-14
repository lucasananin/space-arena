using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_FleeFromTarget", menuName = "SO/State Machines/Actions/AI Flee From Target")]
public class AiFleeFromTargetActionSO : StateActionSO<AiFleeFromTargetAction>
{
}

public class AiFleeFromTargetAction : StateAction
{
    private AiEntity _aiEntity = null;
    private AiEntitySO _aiEntitySO = null;
    private Vector3 _point = default;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiEntitySO = _aiEntity.GetEntitySO<AiEntitySO>();
    }

    public override void OnStateEnter()
    {
        _aiEntity.IsFleeing = true;
        SearchPath();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        if (_aiEntity.HasReachedPathEnding() || !_aiEntity.IsPointCloseToTargetEntity(_point, _aiEntitySO.FleeDistance))
        {
            _aiEntity.IsFleeing = false;
        }
    }

    private void SearchPath()
    {
        _point = _aiEntity.PickRandomPointAwayFromTarget(_aiEntitySO.FleeRange, _aiEntitySO.FleeDistance);
        _aiEntity.SetAIPathDestination(_point);
        _point = _aiEntity.transform.position;
    }
}
