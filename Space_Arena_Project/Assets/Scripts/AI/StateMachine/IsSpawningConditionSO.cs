using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_IsSpawning", menuName = "SO/State Machines/Conditions/Is Spawning")]
public class IsSpawningConditionSO : StateConditionSO<IsSpawningCondition>
{
}

public class IsSpawningCondition : Condition
{
    private SpawnFx _spawnFx = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _spawnFx = _stateMachine.GetComponent<SpawnFx>();
    }

    protected override bool Statement()
    {
        return _spawnFx.IsAnimating;
    }
}
