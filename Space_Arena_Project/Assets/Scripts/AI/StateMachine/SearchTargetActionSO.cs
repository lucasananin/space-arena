using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

public class SearchTargetActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new SearchTargetAction();
    }
}

public class SearchTargetAction : StateAction
{
    private AIDestinationSetter _aIDestinationSetter = null;
    private Collider2D[] _results = new Collider2D[9];
    public LayerMask _layerMask = default;

    public override void Awake(StateMachine stateMachine)
    {
        _aIDestinationSetter = stateMachine.GetComponent<AIDestinationSetter>();
    }

    public override void OnStateEnter()
    {
        var _hits = Physics2D.OverlapCircleNonAlloc(_aIDestinationSetter.transform.position, 99f, _results, _layerMask);
    }

    public override void OnFixedUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}