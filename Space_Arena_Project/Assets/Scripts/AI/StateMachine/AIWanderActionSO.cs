using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AIWander", menuName = "SO/State Machines/Actions/AI Wander")]
public class AIWanderActionSO : StateActionSO
{
    [SerializeField] float _radius = 5f;

    public float Radius { get => _radius; private set => _radius = value; }

    protected override StateAction CreateAction()
    {
        return new AIWanderAction();
    }
}

public class AIWanderAction : StateAction
{
    private new AIWanderActionSO OriginSO => (AIWanderActionSO)base.OriginSO;

    private AIEntity _aIEntity = null;
    private IAstarAI _aiPath = default;

    public override void Awake(StateMachine _stateMachine)
    {
        _aIEntity = _stateMachine.GetComponent<AIEntity>();
        _aiPath = _aIEntity.AiPath;
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all;

        if (CanSearchAnotherPath())
        {
            _aiPath.destination = PickRandomPoint();
            _aiPath.SearchPath();
        }
    }

    private Vector3 PickRandomPoint()
    {
        Vector3 _point = Random.insideUnitCircle * OriginSO.Radius;
        _point += _aiPath.position;
        return _point;
    }

    private bool CanSearchAnotherPath()
    {
        return !_aiPath.pathPending && (_aiPath.reachedEndOfPath || !_aiPath.hasPath);
    }
}