using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_ReadMovementInput", menuName = "SO/State Machines/Actions/Read Movement Input")]
public class ReadMoveInputActionSO : StateActionSO<ReadMoveInputAction>
{
}

public class ReadMoveInputAction : StateAction
{
    private PlayerMover _playerMover = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _playerMover = _stateMachine.GetComponent<PlayerMover>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _playerMover.ReadMovementInput();
    }
}