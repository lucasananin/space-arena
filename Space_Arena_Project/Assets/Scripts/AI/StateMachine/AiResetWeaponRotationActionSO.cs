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
    private AiWeaponHandler _aiWeaponHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiWeaponHandler = _stateMachine.GetComponent<AiWeaponHandler>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _aiWeaponHandler.ResetWeaponRotation();
    }
}
