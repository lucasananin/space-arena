using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_TryShootTarget", menuName = "SO/State Machines/Actions/AI Try Shoot Target")]
public class AiTryShootTargetActionSO : StateActionSO<AiTryShootTargetAction>
{
}

public class AiTryShootTargetAction : StateAction
{
    private AiEntity _aiEntity = null;
    private AiWeaponHandler _aiWeaponHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiWeaponHandler = _stateMachine.GetComponent<AiWeaponHandler>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _aiWeaponHandler.TryShootAll(_aiEntity);
    }
}