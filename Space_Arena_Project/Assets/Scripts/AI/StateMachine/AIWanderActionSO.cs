using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_Wander", menuName = "SO/State Machines/Actions/AI Wander")]
public class AiWanderActionSO : StateActionSO<AiWanderAction>
{
}

public class AiWanderAction : StateAction
{
    private AiEntity _aiEntity = null;
    private AiEntitySO _entitySO = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _entitySO = _aiEntity.GetEntitySO<AiEntitySO>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        //if (_aiEntity.IsWaitingToSearchPath()) return;

        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all;
        bool _canSearchPath = _aiEntity.HasReachedPathEnding() && !_aiEntity.IsWaitingToSearchPath();

        //if (_aiEntity.HasReachedPathEnding())
        if (_canSearchPath)
        {
            SearchPath();
        }
    }

    private void SearchPath()
    {
        Vector3 _point = _aiEntity.PickRandomPointAround(_entitySO.MoveCloseRange);
        _aiEntity.SetAIPathDestination(_point);
        _aiEntity.ResetTimeUntilSearchPath();
    }
}