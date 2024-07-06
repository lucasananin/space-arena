using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_ChargeMove", menuName = "SO/State Machines/Actions/AI Charge Move")]
public class AiChargeMoveActionSO : StateActionSO<AiChargeMoveAction>
{
}

public class AiChargeMoveAction : StateAction
{
    private AiEntity _aiEntity = null;
    //private AiEntitySO _entitySO = null;

    private AIPath _aiPath = null;
    private float _timer = 0f;
    private bool _isCharging = false;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        //_entitySO = _aiEntity.GetEntitySO<AiEntitySO>();
        _aiPath = _stateMachine.GetComponent<AIPath>();
    }

    public override void OnStateEnter()
    {
        // aumenta a velocidade de movimento.
        //SearchPath();
        _timer = 0;
        _isCharging = false;
        _aiEntity.IsFleeing = true;
        _aiEntity.StopAiPath();
        _aiPath.maxSpeed += 3;
    }

    public override void OnStateExit()
    {
        // reseta a velocidade de movimento.
        _aiPath.maxSpeed -= 3;
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > 3)
        {
            _timer = -99;
            _isCharging = true;
            SearchPath();
            Debug.Log($"// start charging");
        }

        bool _canSearchPath = _aiEntity.HasReachedPathEnding() /*&& !_aiEntity.IsWaitingToSearchPath()*/;

        if (_canSearchPath && _isCharging)
        {
            _aiEntity.IsFleeing = false;
            //SearchPath();
            Debug.Log($"// stop charging");
        }
    }

    private void SearchPath()
    {
        // pega uma posicao que atravesse o target.
        var _point = _aiEntity.GetTargetEntityPosition();
        _aiEntity.SetAIPathDestination(_point);
        _aiEntity.ResetTimeUntilSearchPath();
    }
}
