using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_IsAlive", menuName = "SO/State Machines/Conditions/Is Alive")]
public class IsAliveConditionSO : StateConditionSO<IsAliveCondition>
{
}

public class IsAliveCondition : Condition
{
    private EntityBehaviour _entity = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _entity = _stateMachine.GetComponent<EntityBehaviour>();
    }

    protected override bool Statement()
    {
        return _entity.IsAlive();
    }
}