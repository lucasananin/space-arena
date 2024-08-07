using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_Ai_IsFleeing", menuName = "SO/State Machines/Conditions/AI Is Fleeing")]
public class AiIsFleeingConditionSO : StateConditionSO<AiIsFleeingCondition>
{
}

public class AiIsFleeingCondition : Condition
{
    private AiEntity _aiEntity = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
    }

    protected override bool Statement()
    {
        return _aiEntity.IsFleeing;
    }
}
