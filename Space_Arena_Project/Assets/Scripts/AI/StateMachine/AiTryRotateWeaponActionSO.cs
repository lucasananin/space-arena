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
        if (_aiEntity.IsTargetOnLineOfSight)
        {
            _aiWeaponHandler.RotateWeapon(_aiEntity.GetTargetEntityPosition());
        }
        else
        {
            _aiWeaponHandler.ResetWeaponRotation();
        }
    }
}
