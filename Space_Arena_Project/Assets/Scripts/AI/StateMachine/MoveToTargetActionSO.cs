using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AIMoveToTarget", menuName = "SO/State Machines/Actions/AI Move To Target")]
public class MoveToTargetActionSO : StateActionSO
{
    [SerializeField] float _radius = 1f;

    public float Radius { get => _radius; private set => _radius = value; }

    protected override StateAction CreateAction()
    {
        return new MoveToTargetAction();
    }
}

public class MoveToTargetAction : StateAction
{
    private new MoveToTargetActionSO OriginSO => (MoveToTargetActionSO)base.OriginSO;

    private AIEntity _aIEntity = null;
    private IAstarAI _aiPath = default;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AIEntity>();
        _aiPath = _aIEntity.AiPath;
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
        if (_aIEntity.IsCloseToTargetEntity(OriginSO.Radius))
        {
            SetPath();
        }
    }

    private void SetPath()
    {
        //_aiPath.destination = PickRandomPointNearTarget();
        _aiPath.destination = _aIEntity.GetTargetEntityPosition();
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