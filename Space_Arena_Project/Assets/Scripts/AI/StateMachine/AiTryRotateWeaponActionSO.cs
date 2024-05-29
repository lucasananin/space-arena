using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_TryRotateWeapon", menuName = "SO/State Machines/Actions/AI Try Rotate Weapon")]
public class AiTryRotateWeaponActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new AiTryRotateWeaponAction();
    }
}

public class AiTryRotateWeaponAction : StateAction
{
    private AiEntity _aiEntity = null;
    private AiEntitySO _entitySO = null;
    private AIFlipper _aiFlipper = null;
    private AiWeaponHandler _aiWeaponHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiFlipper = _stateMachine.GetComponent<AIFlipper>();
        _aiWeaponHandler = _stateMachine.GetComponent<AiWeaponHandler>();
        _entitySO = _aiEntity.GetEntitySO<AiEntitySO>();
    }

    public override void OnStateEnter()
    {
        _aiFlipper.onFlip += TryResetRotation;
    }

    public override void OnStateExit()
    {
        _aiFlipper.onFlip -= TryResetRotation;
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        TryRotateToTarget();
    }

    private void TryRotateToTarget()
    {
        if (!_entitySO.CanRotateWhileShooting && !_aiWeaponHandler.CanShoot) return;

        if (_aiEntity.IsTargetOnLineOfSight)
        {
            _aiWeaponHandler.RotateWeapon(_aiEntity.GetTargetEntityPosition());
        }
    }

    private void TryResetRotation()
    {
        if (!_aiEntity.IsTargetOnLineOfSight)
        {
            _aiWeaponHandler.ResetWeaponRotation();
        }
    }
}
