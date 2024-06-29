using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_ResetWeaponRotation", menuName = "SO/State Machines/Actions/AI Reset Weapon Rotation")]
public class AiResetWeaponRotationActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new AiResetWeaponRotationAction();
    }
}

public class AiResetWeaponRotationAction : StateAction
{
    private AIFlipper _aiFlipper = null;
    private WeaponRotationHandler _weaponRotationHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiFlipper = _stateMachine.GetComponent<AIFlipper>();
        _weaponRotationHandler = _stateMachine.GetComponent<WeaponRotationHandler>();
    }

    public override void OnStateEnter()
    {
        _aiFlipper.onFlip += ResetWeaponRotation;
    }

    public override void OnStateExit()
    {
        _aiFlipper.onFlip -= ResetWeaponRotation;
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        //
    }

    private void ResetWeaponRotation()
    {
        _weaponRotationHandler.ResetWeaponRotations();
    }
}
