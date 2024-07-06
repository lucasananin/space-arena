using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_Ai_IsTargetOnLineOfSight", menuName = "SO/State Machines/Conditions/AI Is Target On Line Of Sight")]
public class AiIsTargetOnLineOfSightConditionSO : StateConditionSO<AiIsTargetOnLineOfSightCondition>
{
}

public class AiIsTargetOnLineOfSightCondition : Condition
{
    private AiEntity _aiEntity = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
    }

    protected override bool Statement()
    {
        return _aiEntity.IsTargetOnLineOfSight;
    }
}
