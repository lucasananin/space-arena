using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_AIIsFleeing", menuName = "SO/State Machines/Conditions/AI Is Fleeing")]
public class AIIsFleeingConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
        return new IsFleeingCondition();
    }
}

public class IsFleeingCondition : Condition
{
    private AIEntity _aIEntity = null;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AIEntity>();
    }

    protected override bool Statement()
    {
        return _aIEntity.IsFleeing;
    }
}
