using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_Wander", menuName = "SO/State Machines/Actions/AI Wander")]
public class AiWanderActionSO : StateActionSO
{
    [SerializeField] float _radius = 5f;

    public float Radius { get => _radius; private set => _radius = value; }

    protected override StateAction CreateAction()
    {
        return new AiWanderAction();
    }
}

public class AiWanderAction : StateAction
{
    private new AiWanderActionSO OriginSO => (AiWanderActionSO)base.OriginSO;

    private AiEntity _aiEntity = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        if (_aiEntity.IsWaitingToSearchPath()) return;

        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all;

        if (_aiEntity.HasReachedPathEnding())
        {
            SearchPath();
        }
    }

    private void SearchPath()
    {
        Vector3 _point = _aiEntity.PickRandomPointAround(OriginSO.Radius);
        _aiEntity.SetAIPathDestination(_point);
        _aiEntity.ResetTimeUntilSearchPath();
    }
}