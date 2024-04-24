using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_HasTargetEntity", menuName = "SO/State Machines/Conditions/Has Target Entity")]
public class HasTargetEntityConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
        return new HasTargetEntityCondition();
    }
}

public class HasTargetEntityCondition : Condition
{
    private AIWeaponHandler _aiWeaponHandler = null;

    public override void Awake(StateMachine stateMachine)
    {
        _aiWeaponHandler = stateMachine.GetComponent<AIWeaponHandler>();
    }

    protected override bool Statement()
    {
        return _aiWeaponHandler.HasTargetEntity();
    }
}