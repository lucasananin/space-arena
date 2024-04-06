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
    private WeaponRotator _weaponRotator = null;

    public override void Awake(StateMachine stateMachine)
    {
        _weaponRotator = stateMachine.GetComponentInChildren<WeaponRotator>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _weaponRotator.LookAtMouse();
    }
}