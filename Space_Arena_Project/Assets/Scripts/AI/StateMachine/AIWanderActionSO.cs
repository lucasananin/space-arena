using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AIWander", menuName = "SO/State Machines/Actions/AI Wander")]
public class AIWanderActionSO : StateActionSO
{
    [SerializeField] float _radius = 5f;

    //public float Radius => _radius;
    public float Radius { get => _radius; private set => _radius = value; }

    protected override StateAction CreateAction()
    {
        return new AIWanderAction();
    }
}

public class AIWanderAction : StateAction
{
    private new AIWanderActionSO OriginSO => (AIWanderActionSO)base.OriginSO;

    //private StateMachine _stateMachine = null;
    private IAstarAI _aiPath = default;
    //private float _radius = 5;

    //private GameObject _player = null;
    //private GridGraph _graph = null;
    //private int _graphIndex = 0;

    public override void Awake(StateMachine _stateMachine)
    {
        //this._stateMachine = _stateMachine;
        _aiPath = _stateMachine.GetComponent<AIPath>();

        //_graph = AstarPath.active.data.graphs[_graphIndex] as GridGraph;
        //_player = GameObject.FindGameObjectWithTag("Player");
    }

    //public override void OnStateEnter()
    //{
    //    Vector3 _size = new Vector3(5f, 5f, 0f);
    //    Bounds _bounds = new Bounds(_stateMachine.transform.position, _size);
    //    List<GraphNode> _nodes = _graph.GetNodesInRegion(_bounds);

    //    int _count = _nodes.Count;

    //    for (int i = 0; i < _count; i++)
    //    {
    //        Debug.Log($"// _node.position = {_nodes[i].RandomPointOnSurface()}");
    //    }
    //}

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all;

        if (!_aiPath.pathPending && (_aiPath.reachedEndOfPath || !_aiPath.hasPath))
        {
            _aiPath.destination = PickRandomPoint();
            _aiPath.SearchPath();
        }
    }

    private Vector3 PickRandomPoint()
    {
        Vector3 _point = Random.insideUnitSphere * OriginSO.Radius;
        _point.z = 0;
        //_point += _player.transform.position;
        _point += _aiPath.position;
        return _point;
    }

}