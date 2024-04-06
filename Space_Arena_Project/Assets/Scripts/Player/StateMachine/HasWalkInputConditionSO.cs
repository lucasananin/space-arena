using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_HasWalkInput", menuName = "SO/State Machines/Conditions/Has Walk Input")]
public class HasWalkInputConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
        return new HasWalkInputCondition();
    }
}

public class HasWalkInputCondition : Condition
{
    private PlayerMover _playerMover = null;

    public override void Awake(StateMachine stateMachine)
    {
        _playerMover = stateMachine.GetComponent<PlayerMover>();
    }

    protected override bool Statement()
    {
        return _playerMover.HasInputDirection();
    }
}