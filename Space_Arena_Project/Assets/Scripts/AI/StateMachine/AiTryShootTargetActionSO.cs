using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_TryShootTarget", menuName = "SO/State Machines/Actions/AI Try Shoot Target")]
public class AiTryShootTargetActionSO : StateActionSO<AiTryShootTargetAction>
{
    [SerializeField] float _shootDistance = 1f;

    public float ShootDistance { get => _shootDistance; private set => _shootDistance = value; }
}

public class AiTryShootTargetAction : StateAction
{
    private new AiTryShootTargetActionSO OriginSO => (AiTryShootTargetActionSO)base.OriginSO;

    private AiEntity _aIEntity = null;
    //private IAstarAI _aiPath = default;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AiEntity>();
        //_aiPath = _aIEntity.GetAIPath();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        // casta um raycast pra ver se nao tem um obstaculo na frente.
        // ve se o fireRate permite atirar.
        if (_aIEntity.IsCloseToTargetEntity(OriginSO.ShootDistance))
        {
            _aIEntity.PullTrigger();
        }

        _aIEntity.RotateWeaponToTarget();
    }
}