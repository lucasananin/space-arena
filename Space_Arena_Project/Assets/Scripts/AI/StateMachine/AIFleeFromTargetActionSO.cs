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
    private Vector3 _point = default;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AIEntity>();
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
        if (_aIEntity.HasReachedPathEnding() || !_aIEntity.IsPointCloseToTargetEntity(_point, OriginSO.Radius))
        {
            _aIEntity.IsFleeing = false;
        }
    }

    private void SearchPath()
    {
        _point = _aIEntity.PickRandomPointAwayFromTarget(OriginSO.Radius);
        _aIEntity.SetAIPathDestination(_point);
    }
}
