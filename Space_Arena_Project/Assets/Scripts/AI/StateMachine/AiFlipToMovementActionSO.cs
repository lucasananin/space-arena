using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_FlipToMovement", menuName = "SO/State Machines/Actions/AI Flip To Movement")]
public class AiFlipToMovementActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new AiFlipToMovementAction();
    }
}

public class AiFlipToMovementAction : StateAction
{
    private AIFlipper _aiFlipper = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiFlipper = _stateMachine.GetComponent<AIFlipper>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _aiFlipper.FlipToMoveDirection();
    }
}
