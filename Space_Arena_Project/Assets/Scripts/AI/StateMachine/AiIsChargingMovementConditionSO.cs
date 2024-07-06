using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_Ai_IsChargingMovement", menuName = "SO/State Machines/Conditions/AI Is Charging Movement")]
public class AiIsChargingMovementConditionSO : StateConditionSO<AiIsChargingMovementCondition>
{
}

public class AiIsChargingMovementCondition : Condition
{
    private AiEntity _aiEntity = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
    }

    protected override bool Statement()
    {
        return _aiEntity.IsChargingMovement;
    }
}