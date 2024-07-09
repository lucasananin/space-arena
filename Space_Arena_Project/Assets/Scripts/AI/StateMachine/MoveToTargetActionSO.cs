using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AIMoveToTarget", menuName = "SO/State Machines/Actions/AI Move To Target")]
public class MoveToTargetActionSO : StateActionSO<MoveToTargetAction>
{
}

public class MoveToTargetAction : StateAction
{
    private AiEntity _aiEntity = null;
    private AiEntitySO _aiEntitySO = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiEntitySO = _aiEntity.GetEntitySO<AiEntitySO>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        if (_aiEntity.IsCloseToTargetEntity(_aiEntitySO.MinTargetDistance))
        {
            SetPath();
        }
    }

    private void SetPath()
    {
        var _point = _aiEntity.GetTargetEntityPosition();
        _aiEntity.SetAIPathDestination(_point);
    }
}