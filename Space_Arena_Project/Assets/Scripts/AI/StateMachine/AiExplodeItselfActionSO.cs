using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_ExplodeItself", menuName = "SO/State Machines/Actions/AI Explode Itself")]
public class AiExplodeItselfActionSO : StateActionSO<AiExplodeItselfAction>
{
}

public class AiExplodeItselfAction : StateAction
{
    private AiEntity _aiEntity = null;
    private AiEntitySO _aiEntitySO = null;
    private AiWeaponHandler _aiWeaponHandler = null;
    private EnemyHealth _health = null;
    private float _timer = 0f;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiWeaponHandler = _stateMachine.GetComponent<AiWeaponHandler>();
        _aiEntitySO = _aiEntity.GetEntitySO<AiEntitySO>();
        _health = _stateMachine.GetComponent<EnemyHealth>();
    }

    public override void OnStateEnter()
    {
        _timer = 0f;
        _aiEntity.StopAiPath();

        if (_health.IsAlive())
            _health.ForceDie();

        // animacao de enchendo e tremendo.
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > _aiEntitySO.TimeUntilExplode)
        {
            _timer = -99f;
            _aiWeaponHandler.ForceShootAll();
        }
    }
}
