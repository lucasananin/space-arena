using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AITryShootTarget", menuName = "SO/State Machines/Actions/AI Try Shoot Target")]
public class TryShootTargetActionSO : StateActionSO<TryShootTargetAction>
{
    [SerializeField] float _shootDistance = 1f;

    public float ShootDistance { get => _shootDistance; private set => _shootDistance = value; }
}

public class TryShootTargetAction : StateAction
{
    private new TryShootTargetActionSO OriginSO => (TryShootTargetActionSO)base.OriginSO;

    private AIEntity _aIEntity = null;
    //private IAstarAI _aiPath = default;

    public override void Awake(StateMachine stateMachine)
    {
        _aIEntity = stateMachine.GetComponent<AIEntity>();
        //_aiPath = _aIEntity.GetAIPath();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        // casta um raycast pra ver se nao tem um obstaculo na frente.
        if (_aIEntity.IsCloseToTargetEntity(OriginSO.ShootDistance))
        {
            _aIEntity.PullTrigger();
        }

        // rotacionar a arma na direcao do target.
    }
}