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

    private AiEntity _aIEntity = null;
    private AiEntitySO _entitySO = null;
    private Vector3 _point = default;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AiEntity>();
        _entitySO = _aIEntity.GetEntitySO<AiEntitySO>();
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
        if (_entitySO.StopOnCloseEnough && _aIEntity.IsCloseToTargetEntity(_entitySO.MoveClose_radius)) return;

        if (_aIEntity.HasReachedPathEnding() || !_aIEntity.IsPointCloseToTargetEntity(_point, _entitySO.MoveClose_radius))
        {
            SearchPath();
        }
    }

    private void SearchPath()
    {
        // Pega uma posicao onde o target possa ser visto. Castar um raycast pra ver se nao tem nada na frente.
        // Colocar um numero maximo de tentativas para evitar lacos infinitos.
        _point = _aIEntity.PickRandomPointNearTarget(_entitySO.MoveClose_radius);
        _aIEntity.SetAIPathDestination(_point);
    }
}