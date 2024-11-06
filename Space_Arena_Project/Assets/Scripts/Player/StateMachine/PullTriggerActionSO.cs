using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_PullTrigger", menuName = "SO/State Machines/Actions/Pull Trigger")]
public class PullTriggerActionSO : StateActionSO<PullTriggerAction>
{
}

public class PullTriggerAction : StateAction
{
    private PlayerWeaponHandler _weaponHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _weaponHandler = _stateMachine.GetComponent<PlayerWeaponHandler>();
    }

    public override void OnStateEnter()
    {
        InputHandler.OnLeftMouseButtonDown += _weaponHandler.PullTrigger;
        InputHandler.OnLeftMouseButtonUp += _weaponHandler.ReleaseTrigger;
    }

    public override void OnStateExit()
    {
        InputHandler.OnLeftMouseButtonDown -= _weaponHandler.PullTrigger;
        InputHandler.OnLeftMouseButtonUp -= _weaponHandler.ReleaseTrigger;
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
