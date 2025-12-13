using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_SwapWeapon", menuName = "SO/State Machines/Actions/Swap Weapon")]
public class SwapWeaponActionSO : StateActionSO<SwapWeaponAction>
{
}

public class SwapWeaponAction : StateAction
{
    private PlayerWeaponHandler _weaponHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _weaponHandler = _stateMachine.GetComponent<PlayerWeaponHandler>();
    }

    public override void OnStateEnter()
    {
        InputHandler.OnMouseScrollSwipe += _weaponHandler.SwapThroughInput;
        InputHandler.OnWeaponChange += () => _weaponHandler.SwapThroughInput(1);
    }

    public override void OnStateExit()
    {
        InputHandler.OnMouseScrollSwipe -= _weaponHandler.SwapThroughInput;
        InputHandler.OnWeaponChange -= () => _weaponHandler.SwapThroughInput(1);
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        //
    }
}