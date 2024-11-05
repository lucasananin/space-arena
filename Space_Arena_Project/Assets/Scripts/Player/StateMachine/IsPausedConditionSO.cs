using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_IsPaused", menuName = "SO/State Machines/Conditions/Is Paused")]
public class IsPausedConditionSO : StateConditionSO<IsPausedCondition>
{
}

public class IsPausedCondition : Condition
{
    private bool _isPaused = false;

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        PausePanel.OnPause += Pause;
        PausePanel.OnUnpause += Unpause;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        PausePanel.OnPause -= Pause;
        PausePanel.OnUnpause -= Unpause;
    }

    private void Pause()
    {
        _isPaused = true;
    }

    private void Unpause()
    {
        _isPaused = false;
    }

    protected override bool Statement()
    {
        return _isPaused;
    }
}