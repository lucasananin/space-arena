using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AIFleeFromTarget", menuName = "SO/State Machines/Actions/AI Flee From Target")]
public class AIFleeFromTargetActionSO : StateActionSO
{
    [SerializeField] float _radius = 1f;

    public float Radius { get => _radius; private set => _radius = value; }

    protected override StateAction CreateAction()
    {
        return new AIFleeFromTargetAction();
    }
}

public class AIFleeFromTargetAction : StateAction
{
    private new AIFleeFromTargetActionSO OriginSO => (AIFleeFromTargetActionSO)base.OriginSO;

    private AIEntity _aIEntity = null;
    private IAstarAI _aiPath = default;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AIEntity>();
        _aiPath = _aIEntity.AiPath;
    }

    public override void OnStateEnter()
    {
        _aIEntity.IsFleeing = true;
        SearchPath();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        if (HasReachedEndPath())
        {
            _aIEntity.IsFleeing = false;
        }
    }

    private void SearchPath()
    {
        _aiPath.destination = PickRandomPointAwayFromTarget();
        _aiPath.SearchPath();
    }

    private Vector3 PickRandomPointAwayFromTarget()
    {
        Vector3 _point = Random.insideUnitCircle * OriginSO.Radius;
        Vector3 _center = (_aIEntity.transform.position - _aIEntity.GetTargetEntityPosition()).normalized * OriginSO.Radius;
        _point += _center;
        Debug.Log($"// _fleePointCenter = {_center}");
        return _point;
    }

    private bool HasReachedEndPath()
    {
        return !_aiPath.pathPending && (_aiPath.reachedEndOfPath || !_aiPath.hasPath);
    }
}
