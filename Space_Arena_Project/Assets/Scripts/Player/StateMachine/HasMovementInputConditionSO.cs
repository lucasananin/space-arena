using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_HasMovementInput", menuName = "SO/State Machines/Conditions/Has Movement Input")]
public class HasMovementInputConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
        return new HasMovementInputCondition();
    }
}

public class HasMovementInputCondition : Condition
{
    private PlayerMover _playerMover = null;

    public override void Awake(StateMachine stateMachine)
    {
        _playerMover = stateMachine.GetComponent<PlayerMover>();
    }

    protected override bool Statement()
    {
        return _playerMover.HasMovementInput();
    }
}