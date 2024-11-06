using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_PlayerAim", menuName = "SO/State Machines/Actions/Player Aim")]
public class PlayerAimActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new PlayerAimAction();
    }
}

public class PlayerAimAction : StateAction
{
    private PlayerWeaponHandler _playerWeaponHandler = null;
    private PlayerFlipper _playerFlipper = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _playerWeaponHandler = _stateMachine.GetComponent<PlayerWeaponHandler>();
        _playerFlipper = _stateMachine.GetComponent<PlayerFlipper>();
    }

    public override void OnStateEnter()
    {
        _playerWeaponHandler.CanRotateWeapon = true;
    }

    public override void OnStateExit()
    {
        _playerWeaponHandler.CanRotateWeapon = false;
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        //_playerWeaponHandler.RotateCurrentWeapon();
        _playerFlipper.FlipToMouse();
    }
}