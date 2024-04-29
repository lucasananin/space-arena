using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_FleeFromTarget", menuName = "SO/State Machines/Actions/AI Flee From Target")]
public class AiFleeFromTargetActionSO : StateActionSO
{
    [SerializeField] float _radius = 1f;
    [SerializeField] float _distance = 1f;

    public float Radius { get => _radius; private set => _radius = value; }
    public float Distance { get => _distance; private set => _distance = value; }

    protected override StateAction CreateAction()
    {
        return new AiFleeFromTargetAction();
    }
}

public class AiFleeFromTargetAction : StateAction
{
    private new AiFleeFromTargetActionSO OriginSO => (AiFleeFromTargetActionSO)base.OriginSO;

    private AiEntity _aIEntity = null;
    private Vector3 _point = default;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AiEntity>();
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
        if (_aIEntity.HasReachedPathEnding() || !_aIEntity.IsPointCloseToTargetEntity(_point, OriginSO.Distance))
        {
            _aIEntity.IsFleeing = false;
        }
    }

    private void SearchPath()
    {
        _point = _aIEntity.PickRandomPointAwayFromTarget(OriginSO.Radius, OriginSO.Distance);
        _aIEntity.SetAIPathDestination(_point);
        _point = _aIEntity.transform.position;
    }
}
