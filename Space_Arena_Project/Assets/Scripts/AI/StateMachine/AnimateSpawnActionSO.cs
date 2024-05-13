using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AnimateSpawn", menuName = "SO/State Machines/Actions/Animate Spawn")]
public class AnimateSpawnActionSO : StateActionSO<AnimateSpawnAction>
{
}

public class AnimateSpawnAction : StateAction
{
    private SpawnFx _spawnFx = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _spawnFx = _stateMachine.GetComponent<SpawnFx>();
    }

    public override void OnStateEnter()
    {
        _spawnFx.Play();
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
    }
}
