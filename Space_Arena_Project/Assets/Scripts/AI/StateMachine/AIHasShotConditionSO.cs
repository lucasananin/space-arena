using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_AIHasShot", menuName = "SO/State Machines/Conditions/AI Has Shot")]
public class AIHasShotConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
        return new AIHasShotCondition();
    }
}

public class AIHasShotCondition : Condition
{
    //private AIEntity _aIEntity = null;
    private AIWeaponHandler _aIWeaponHandler = null;
    private bool _hasShot = false;

    public override void Awake(StateMachine _stateMachine)
    {
        _aIWeaponHandler = _stateMachine.GetComponent<AIWeaponHandler>();
    }

    public override void OnStateEnter()
    {
        _aIWeaponHandler.onStoppedShooting += _aIWeaponHandler_onStoppedShooting;
    }

    public override void OnStateExit()
    {
        _aIWeaponHandler.onStoppedShooting -= _aIWeaponHandler_onStoppedShooting;
        _hasShot = false;
    }

    private void _aIWeaponHandler_onStoppedShooting()
    {
        _hasShot = true;
    }

    protected override bool Statement()
    {
        return _hasShot;
    }
}