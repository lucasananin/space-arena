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
    //private AiEntitySO _entitySO = null;
    private AiWeaponHandler _aiWeaponHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        //_entitySO = _aiEntity.GetEntitySO<AiEntitySO>();

        if (_stateMachine.TryGetComponent(out AiWeaponHandler _aiWeaponHandler))
            this._aiWeaponHandler = _aiWeaponHandler;
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _aiWeaponHandler.TryShootAll(_aiEntity);

        //if (_aiWeaponHandler.IsShooting) return;
        //if (!_entitySO.CanShootWhileMoving && _aiEntity.IsMoving()) return;

        //if (_aiEntity.IsTargetOnLineOfSight && _aiEntity.IsCloseToTargetEntity(_entitySO.ShootDistance))
        //{
        //    _aiWeaponHandler.StartShooting();
        //}
    }
}