using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_TryFlipToTarget", menuName = "SO/State Machines/Actions/AI Try Flip To Target")]
public class AiTryFlipToTargetActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new AiTryFlipToTargetAction();
    }
}

public class AiTryFlipToTargetAction : StateAction
{
    private AiEntity _aiEntity = null;
    private AIFlipper _aiFlipper = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiFlipper = _stateMachine.GetComponent<AIFlipper>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        if (_aiEntity.IsTargetOnLineOfSight)
        {
            _aiFlipper.FlipToX(_aiEntity.GetTargetEntityPosition().x, _aiEntity.transform.position.x);
        }
        else
        {
            _aiFlipper.FlipToMoveDirection();
        }
    }
}
