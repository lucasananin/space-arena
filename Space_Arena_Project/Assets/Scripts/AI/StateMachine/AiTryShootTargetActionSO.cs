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
    private AiEntity _aIEntity = null;
    private AiEntitySO _entitySO = null;
    private AiWeaponHandler _aiWeaponHandler = null;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AiEntity>();
        _aiWeaponHandler = stateMachine.GetComponent<AiWeaponHandler>();
        _entitySO = _aIEntity.GetEntitySO<AiEntitySO>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        // Ver se o fireRate permite atirar.
        if (_aIEntity.IsTargetOnLineOfSight && _aIEntity.IsCloseToTargetEntity(_entitySO.ShootDistance))
        {
            _aiWeaponHandler.PullTrigger();
        }
    }
}