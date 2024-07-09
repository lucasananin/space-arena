using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Ai_Wander", menuName = "SO/State Machines/Actions/AI Wander")]
public class AiWanderActionSO : StateActionSO<AiWanderAction>
{
}

public class AiWanderAction : StateAction
{
    private AiEntity _aiEntity = null;
    private AiEntitySO _entitySO = null;
    //private AIPath _aiPath = null;
    private GridGraph _graph = null;
    private const int GRAPH_INDEX = 0;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _entitySO = _aiEntity.GetEntitySO<AiEntitySO>();
        //_aiPath = _aiEntity.GetComponent<AIPath>();
        _graph = AstarPath.active.data.graphs[GRAPH_INDEX] as GridGraph;
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all;
        bool _canSearchPath = _aiEntity.HasReachedPathEnding() && !_aiEntity.IsWaitingToSearchPath();

        //if (_aiEntity.HasReachedPathEnding())
        if (_canSearchPath)
        {
            SearchPath();
        }
    }

    private void SearchPath()
    {
        //Vector3 _point = _aiEntity.PickRandomPointAround(_entitySO.MoveCloseRange);
        var _point = GetPosition();
        _aiEntity.SetAIPathDestination(_point);
        _aiEntity.ResetTimeUntilSearchPath();
    }

    private Vector3 GetPosition()
    {
        // bota os points em uma lista e procura um posicao que esteja longe.
        // limitar o tamanho da lista.

        Vector3 _position = default;
        var _foundNode = false;

        do
        {
            var _nodes = _graph.nodes;
            int _randomIndex = Random.Range(0, _nodes.Length);
            var _node = _graph.nodes[_randomIndex];
            Debug.Log($"// A");

            if (!_node.Walkable) continue;

            var _nodePosition = (Vector3)_node.position;
            var _myPosition = _aiEntity.transform.position;
            var _isTooClose = GeneralMethods.IsPointCloseToTarget(_myPosition, _nodePosition, _entitySO.MoveCloseRange.x);
            var _isTooFar = !GeneralMethods.IsPointCloseToTarget(_myPosition, _nodePosition, _entitySO.MoveCloseRange.y);
            Debug.Log($"// B");

            if (_isTooClose || _isTooFar) continue;

            _foundNode = true;
            _position = _nodePosition;
            Debug.Log($"// C");

        } while (!_foundNode);

        return _position;
    }
}