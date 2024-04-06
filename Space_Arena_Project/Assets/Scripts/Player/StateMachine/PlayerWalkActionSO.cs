using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_PlayerWalk", menuName = "SO/State Machines/Actions/Player Walk")]
public class PlayerWalkActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new PlayerWalkAction();
    }
}

public class PlayerWalkAction : StateAction
{
    private PlayerMover _playerMover = null;

    public override void Awake(StateMachine stateMachine)
    {
        _playerMover = stateMachine.GetComponent<PlayerMover>();
    }

    public override void OnFixedUpdate()
    {
        _playerMover.Move();
    }

    public override void OnUpdate()
    {
        //
    }

    public override void OnStateExit()
    {
        _playerMover.ResetVelocity();
    }
}