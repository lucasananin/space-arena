using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AIMoveToTarget", menuName = "SO/State Machines/Actions/AI Move To Target")]
public class MoveToTargetActionSO : StateActionSO<MoveToTargetAction>
{
    //[SerializeField] float _radius = 1f;

    //public float Radius { get => _radius; private set => _radius = value; }

    //protected override StateAction CreateAction()
    //{
    //    return new MoveToTargetAction();
    //}
}

public class MoveToTargetAction : StateAction
{
    //private new MoveToTargetActionSO OriginSO => (MoveToTargetActionSO)base.OriginSO;

    private AiEntity _aiEntity = null;
    private AiEntitySO _aiEntitySO = null;
    private IAstarAI _aiPath = default;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiEntitySO = _aiEntity.GetEntitySO<AiEntitySO>();
        _aiPath = _aiEntity.AiPath;
    }

    //public override void OnStateEnter()
    //{
    //    SetPath();
    //}

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        //if (_aiEntity.IsCloseToTargetEntity(OriginSO.Radius))
        if (_aiEntity.IsCloseToTargetEntity(_aiEntitySO.MinTargetDistance))
        {
            SetPath();
        }
    }

    private void SetPath()
    {
        //_aiPath.destination = PickRandomPointNearTarget();
        _aiPath.destination = _aiEntity.GetTargetEntityPosition();
        _aiPath.SearchPath();
    }

    //private Vector3 PickRandomPointNearTarget()
    //{
    //    Vector3 _point = Random.insideUnitCircle * 0.5f;
    //    _point += _aIEntity.GetTargetEntityPosition();
    //    return _point;
    //}

    //private bool IsCloseEnough()
    //{
    //    float _distance = (_aIEntity.GetTargetEntityPosition() - _aIEntity.transform.position).sqrMagnitude;
    //    return _distance < OriginSO.Radius * OriginSO.Radius;
    //}
}