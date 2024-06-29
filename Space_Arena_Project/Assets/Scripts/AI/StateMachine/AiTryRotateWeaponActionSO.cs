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
    private WeaponRotationHandler _weaponRotationHandler = null;
    private MultiWeaponHandler _multiWeaponHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiFlipper = _stateMachine.GetComponent<AIFlipper>();
        _entitySO = _aiEntity.GetEntitySO<AiEntitySO>();
        _weaponRotationHandler = _stateMachine.GetComponent<WeaponRotationHandler>();

        if (_stateMachine.TryGetComponent(out AiWeaponHandler _aiWeaponHandler))
            this._aiWeaponHandler = _aiWeaponHandler;

        if (_stateMachine.TryGetComponent(out MultiWeaponHandler _multiWeaponHandler))
            this._multiWeaponHandler = _multiWeaponHandler;
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
        //if (_multiWeaponHandler is not null)
        //{
        //    if (_aiEntity.IsTargetOnLineOfSight)
        //    {
        //        _weaponRotationHandler.RotateWeapons(_aiEntity.GetTargetEntityPosition());
        //    }
        //}
        //else
        //{
        //    if (!_entitySO.CanRotateWhileShooting && _aiWeaponHandler.IsShooting) return;

        //    if (_aiEntity.IsTargetOnLineOfSight)
        //    {
        //        _weaponRotationHandler.RotateWeapons(_aiEntity.GetTargetEntityPosition());
        //    }
        //}

        if (_aiEntity.IsTargetOnLineOfSight)
        {
            _aiWeaponHandler.RotateWeapons(_aiEntity.GetTargetEntityPosition());
        }
    }

    private void TryResetRotation()
    {
        if (!_aiEntity.IsTargetOnLineOfSight)
        {
            _aiWeaponHandler.ResetWeaponRotations();
            //_weaponRotationHandler.ResetWeaponRotations();
        }
    }
}
