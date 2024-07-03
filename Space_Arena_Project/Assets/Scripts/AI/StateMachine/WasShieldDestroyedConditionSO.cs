using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_WasShieldDestroyed", menuName = "SO/State Machines/Conditions/Was Shield Destroyed")]
public class WasShieldDestroyedConditionSO : StateConditionSO<WasShieldDestroyedCondition>
{
}

public class WasShieldDestroyedCondition : Condition
{
    private ShieldHealth _shieldHealth = null;
    private bool _wasDestroyed = false;

    public override void Awake(StateMachine _stateMachine)
    {
        _shieldHealth = _stateMachine.GetComponentInChildren<ShieldHealth>();
    }

    public override void OnStateEnter()
    {
        _shieldHealth.OnDead += EnableWasDestroyed;
    }

    public override void OnStateExit()
    {
        _shieldHealth.OnDead -= EnableWasDestroyed;
        _wasDestroyed = false;
    }

    private void EnableWasDestroyed()
    {
        _wasDestroyed = true;
    }

    protected override bool Statement()
    {
        return _wasDestroyed;
    }
}