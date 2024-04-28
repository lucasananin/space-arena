using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_ResetWeaponRotation", menuName = "SO/State Machines/Actions/AI Reset Weapon Rotation")]
public class AiResetWeaponRotationActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new AiResetWeaponRotationAction();
    }
}

public class AiResetWeaponRotationAction : StateAction
{
    private AiEntity _aiEntity = null;
    private AiWeaponHandler _aiWeaponHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiWeaponHandler = _stateMachine.GetComponent<AiWeaponHandler>();
    }

    public override void OnStateEnter()
    {
        _aiEntity.GetComponent<SideFlipper>().onFlip += AiTryRotateWeaponAction_onFlip;
    }

    public override void OnStateExit()
    {
        _aiEntity.GetComponent<SideFlipper>().onFlip -= AiTryRotateWeaponAction_onFlip;
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        //_aiWeaponHandler.RotateWeaponToDirection(_aiEntity.GetMoveDirection());
        //_aiWeaponHandler.ResetWeaponRotation();
    }

    private void AiTryRotateWeaponAction_onFlip()
    {
        _aiWeaponHandler.ResetWeaponRotation();
    }
}
