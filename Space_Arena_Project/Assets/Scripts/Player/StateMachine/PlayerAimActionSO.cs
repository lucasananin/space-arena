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

    public override void Awake(StateMachine stateMachine)
    {
        _playerWeaponHandler = stateMachine.GetComponent<PlayerWeaponHandler>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _playerWeaponHandler.RotateCurrentWeapon();
    }
}