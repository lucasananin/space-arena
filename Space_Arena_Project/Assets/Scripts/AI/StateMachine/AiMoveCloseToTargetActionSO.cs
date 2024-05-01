using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_MoveCloserToTarget", menuName = "SO/State Machines/Actions/AI Move Closer To Target")]
public class AiMoveCloseToTargetActionSO : StateActionSO
{
    //[SerializeField] float _radius = 1f;
    //[SerializeField] bool _stopOnCloseEnough = true;

    //public float Radius { get => _radius; private set => _radius = value; }
    //public bool StopOnCloseEnough { get => _stopOnCloseEnough; private set => _stopOnCloseEnough = value; }

    protected override StateAction CreateAction()
    {
        return new AiMoveCloseToTargetAction();
    }
}

public class AiMoveCloseToTargetAction : StateAction
{
    //private new AiMoveCloseToTargetActionSO OriginSO => (AiMoveCloseToTargetActionSO)base.OriginSO;

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
        if (_entitySO.StopOnCloseEnough && _aiEntity.IsCloseToTargetEntity(_entitySO.MoveClose_radius)) return;

        if (_aiEntity.HasReachedPathEnding() || !_aiEntity.IsPointCloseToTargetEntity(_point, _entitySO.MoveClose_radius))
        {
            SearchPath();
        }
    }

    private void SearchPath()
    {
        //for (int i = 0; i < _entitySO.MaxNumberOfTries; i++)
        //{
        //    _point = _aiEntity.PickRandomPointNearTarget(_entitySO.MoveClose_radius);

        //    if (_aiEntity.CanSeeTargetFromPoint(_point))
        //    {
        //        _aiEntity.SetAIPathDestination(_point);
        //        Debug.Log($"// A: point = {_point}, index = {i}");
        //        return;
        //    }
        //}

        _point = _aiEntity.PickRandomPointNearTarget(_entitySO.MoveClose_radius);
        _aiEntity.SetAIPathDestination(_point);
        //Debug.Log($"// B");
    }
}