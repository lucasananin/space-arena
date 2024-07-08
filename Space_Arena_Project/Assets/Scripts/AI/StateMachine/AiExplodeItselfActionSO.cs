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
    private AiWeaponHandler _aiWeaponHandler = null;
    private float _timer = 0f;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiWeaponHandler = _stateMachine.GetComponent<AiWeaponHandler>();
    }

    public override void OnStateEnter()
    {
        _timer = 0f;
        _aiEntity.StopAiPath();
        // animacao de enchendo e tremendo.
        // force shoot all.
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.7f)
        {
            _timer = -123f;
            _aiWeaponHandler.ForceShootAll();
        }
    }
}
