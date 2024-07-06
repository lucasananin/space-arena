using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_Ai_IsWaitingBullCharge", menuName = "SO/State Machines/Conditions/AI Is Is Waiting Bull Charge")]
public class AiIsWaitingBullChargeActionSO : StateConditionSO<AiIsWaitingBullChargeCondition>
{
}

public class AiIsWaitingBullChargeCondition : Condition
{
    private AiEntity _aiEntity = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
    }

    protected override bool Statement()
    {
        return _aiEntity.IsWaitingBullCharge;
    }
}
