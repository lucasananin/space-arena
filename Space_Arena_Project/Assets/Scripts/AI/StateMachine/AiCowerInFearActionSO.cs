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
    private AiEntitySO _aiEntitySO = null;
    private ShieldHealth _shieldHealth = null;
    //private CowerInFearVfx _cowerInFearVfx = null;
    private float _timer = 0f;
    private float _timeUntilEnd = 0f;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiEntitySO = _aiEntity.GetEntitySO<AiEntitySO>();
        _shieldHealth = _stateMachine.GetComponentInChildren<ShieldHealth>();
        //_cowerInFearVfx = _stateMachine.GetComponent<CowerInFearVfx>();
    }

    public override void OnStateEnter()
    {
        ResetTime();
        _aiEntity.StopAiPath();
        _aiEntity.IsCowering = true;
        _shieldHealth.OnDamageTaken += ResetTime;
        _shieldHealth.OnDead += ResetTime;
    }

    public override void OnStateExit()
    {
        _shieldHealth.OnDamageTaken -= ResetTime;
        _shieldHealth.OnDead -= ResetTime;
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > _timeUntilEnd)
        {
            _aiEntity.IsCowering = false;
        }
    }

    private void ResetTime()
    {
        _timer = 0f;
        _timeUntilEnd = Random.Range(_aiEntitySO.CowerTimeRange.x, _aiEntitySO.CowerTimeRange.y);
        //_cowerInFearVfx.Play(_timeUntilEnd);
    }
}
