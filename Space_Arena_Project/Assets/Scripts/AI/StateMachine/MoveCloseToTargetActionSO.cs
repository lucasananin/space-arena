using Pathfinding;
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
    private IAstarAI _aiPath = default;

    private float _timer = 0f;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AIEntity>();
        _aiPath = _aIEntity.GetAIPath();
    }

    public override void OnStateEnter()
    {
        _timer = OriginSO.MoveRate;
        SearchPath();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (OriginSO.StopOnCloseEnough && _aIEntity.IsCloseToTargetEntity(OriginSO.Radius)) return;

        //if (IsCloseEnough())
        //{
        //    if (CanSearchAnotherPath())
        //    {
        //        _aiPath.destination = _aIEntity.GetTargetEntityPosition();
        //        _aiPath.SearchPath();
        //    }
        //}
        //else
        //{
        //    if (CanSearchAnotherPath())
        //    //if (_timer > OriginSO.MoveRate)
        //    {
        //        //_timer = 0f;
        //        SearchPath();
        //    }
        //}

        //if (CanSearchAnotherPath())
        if (_timer > OriginSO.MoveRate)
        {
            _timer = 0f;
            SearchPath();
        }
    }

    private void SearchPath()
    {
        // Pega uma posicao onde o target possa ser visto. Castar um raycast pra ver se nao tem nada na frente.
        // colocar um numero maximo de tentativas para evitar lacos infinitos.

        _aiPath.destination = PickRandomPointNearTarget();
        _aiPath.SearchPath();
    }

    private Vector3 PickRandomPointNearTarget()
    {
        Vector3 _point = Random.insideUnitCircle * OriginSO.Radius;
        _point += _aIEntity.GetTargetEntityPosition();
        return _point;
    }

    //private bool IsCloseEnough()
    //{
    //    float _distance = (_aIEntity.GetTargetEntityPosition() - _aIEntity.transform.position).sqrMagnitude;
    //    return _distance < OriginSO.Radius * OriginSO.Radius;
    //}

    private bool CanSearchAnotherPath()
    {
        return !_aiPath.pathPending && (_aiPath.reachedEndOfPath || !_aiPath.hasPath);
    }
}