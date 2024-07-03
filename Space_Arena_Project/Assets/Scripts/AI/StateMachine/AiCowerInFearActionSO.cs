using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_CowerInFear", menuName = "SO/State Machines/Actions/AI Cower In Fear")]
public class AiCowerInFearActionSO : StateActionSO<AiCowerInFearAction>
{
}

public class AiCowerInFearAction : StateAction
{
    private AiEntity _aiEntity = null;
    private float _timer = 0f;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
    }

    public override void OnStateEnter()
    {
        _timer = 0f;
        _aiEntity.StopAiPath();
        _aiEntity.IsCowering = true;
        // se o escudo for atingido, zera o timer.
        // tremiliqueVfx.
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > 3f)
        {
            _aiEntity.IsCowering = false;
        }
    }
}
