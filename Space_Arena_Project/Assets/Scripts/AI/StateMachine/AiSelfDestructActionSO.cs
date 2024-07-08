using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using DG.Tweening;

[CreateAssetMenu(fileName = "Action_Ai_SelfDestruct", menuName = "SO/State Machines/Actions/AI Self Destruct")]
public class AiSelfDestructActionSO : StateActionSO<AiSelfDestructAction>
{
}

public class AiSelfDestructAction : StateAction
{
    private AiEntity _aiEntity = null;
    private AiEntitySO _aiEntitySO = null;
    private AiWeaponHandler _aiWeaponHandler = null;
    private EnemyHealth _health = null;
    private float _timer = 0f;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _aiWeaponHandler = _stateMachine.GetComponent<AiWeaponHandler>();
        _aiEntitySO = _aiEntity.GetEntitySO<AiEntitySO>();
        _health = _stateMachine.GetComponent<EnemyHealth>();
    }

    public override void OnStateEnter()
    {
        _timer = 0f;
        _aiEntity.StopAiPath();

        if (_health.IsAlive())
            _health.ForceDie();

        // Transformar isso em um script próprio.
        var _transform = _aiEntity.transform;
        var _scale = _transform.localScale;
        _transform.DOScale(_scale * 1.5f, _aiEntitySO.SelfDestructTime);
        //_transform.DOShakePosition(_aiEntitySO.TimeUntilExplode);
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > _aiEntitySO.SelfDestructTime)
        {
            _timer = -99f;
            _aiWeaponHandler.ForceShootAll();
        }
    }
}
