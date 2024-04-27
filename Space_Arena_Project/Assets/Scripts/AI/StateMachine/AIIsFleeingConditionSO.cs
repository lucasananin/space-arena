using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_Ai_IsFleeing", menuName = "SO/State Machines/Conditions/AI Is Fleeing")]
public class AiIsFleeingConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
        return new AiIsFleeingCondition();
    }
}

public class AiIsFleeingCondition : Condition
{
    private AiEntity _aIEntity = null;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AiEntity>();
    }

    protected override bool Statement()
    {
        return _aIEntity.IsFleeing;
    }
}
