using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_IsCowering", menuName = "SO/State Machines/Conditions/Is Cowering")]
public class IsCoweringConditionSO : StateConditionSO<IsCoweringCondition>
{
}

public class IsCoweringCondition : Condition
{
    private AiEntity _aiEntity = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
    }

    protected override bool Statement()
    {
        return _aiEntity.IsCowering;
    }
}