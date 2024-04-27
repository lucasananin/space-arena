using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AIMoveCloserToTarget", menuName = "SO/State Machines/Actions/AI Move Closer To Target")]
public class MoveCloseToTargetActionSO : StateActionSO
{
    [SerializeField] float _radius = 1f;
    [SerializeField] float _moveRate = 1f;
    [SerializeField] bool _stopOnCloseEnough = true;

    public float Radius { get => _radius; private set => _radius = value; }
    public float MoveRate { get => _moveRate; private set => _moveRate = value; }
    public bool StopOnCloseEnough { get => _stopOnCloseEnough; private set => _stopOnCloseEnough = value; }

    protected override StateAction CreateAction()
    {
        return new MoveCloseToTargetAction();
    }
}

public class MoveCloseToTargetAction : StateAction
{
    private new MoveCloseToTargetActionSO OriginSO => (MoveCloseToTargetActionSO)base.OriginSO;

    private AIEntity _aIEntity = null;
    private Vector3 _point = default;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AIEntity>();
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
        if (OriginSO.StopOnCloseEnough && _aIEntity.IsCloseToTargetEntity(OriginSO.Radius)) return;

        if (_aIEntity.HasReachedPathEnding() || !_aIEntity.IsPointCloseToTargetEntity(_point, OriginSO.Radius))
        {
            SearchPath();
        }
    }

    private void SearchPath()
    {
        // Pega uma posicao onde o target possa ser visto. Castar um raycast pra ver se nao tem nada na frente.
        // colocar um numero maximo de tentativas para evitar lacos infinitos.
        _point = _aIEntity.PickRandomPointNearTarget(OriginSO.Radius);
        _aIEntity.SetAIPathDestination(_point);
    }
}