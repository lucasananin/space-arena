using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphUpdateTest : MonoBehaviour
{
    public GridGraph _graph;
    public int _graphIndex;
    public GameObject[] _walls = null;
    //public List<Vector2Int> _wallGridPositions = null;

    private void Start()
    {
        if (AstarPath.active == null)
        {
            throw new System.Exception("There is no AstarPath object in the scene");
        }

        // If one creates this component via a script then they may have already set the graph field.
        // In that case don't replace it.
        if (_graph == null)
        {
            if (_graphIndex < 0)
            {
                throw new System.Exception("Graph index should not be negative");
            }

            if (_graphIndex >= AstarPath.active.data.graphs.Length)
            {
                throw new System.Exception("The ProceduralGridMover was configured to use graph index " + _graphIndex + ", but only " + AstarPath.active.data.graphs.Length + " graphs exist");
            }

            _graph = AstarPath.active.data.graphs[_graphIndex] as GridGraph;

            if (_graph == null)
            {
                throw new System.Exception("The ProceduralGridMover was configured to use graph index " + _graphIndex + " but that graph either does not exist or is not a GridGraph or LayerGridGraph");
            }
        }
    }

    [Button]
    private void AlternateWallsVisibility()
    {
        for (int i = 0; i < _walls.Length; i++)
        {
            _walls[i].SetActive(!_walls[i].activeSelf);
        }

        //_wallGridPositions.Clear();

        //for (int i = 0; i < _walls.Length; i++)
        //{
        //    Vector2 _wallPositon2D = _walls[i].transform.position;
        //    Vector2Int _wallGridPosition = new Vector2Int((int)_wallPositon2D.x, (int)_wallPositon2D.y);
        //    _wallGridPositions.Add(_wallGridPosition);
        //}
    }

    [Button]
    private void UpdateGraph()
    {
        //// Recalculate only the first grid graph
        //var graphToScan = AstarPath.active.data.gridGraph;
        //AstarPath.active.Scan(graphToScan);

        StartCoroutine(UpdateGraphCoroutine());
    }

    private IEnumerator UpdateGraphCoroutine()
    {
        int _width = _graph.width;
        int _depth = _graph.depth;

        int _yieldEvery = Mathf.Max(_depth * _width / 20, 1000);
        int _counter = 0;

        // Just update all nodes
        for (int z = 0; z < _depth; z++)
        {
            for (int x = 0; x < _width; x++)
            {
                _graph.RecalculateCell(x, z);
            }

            _counter += _width;

            if (_counter > _yieldEvery)
            {
                _counter = 0;
                yield return null;
            }
        }

        // Recalculate the connections of all nodes
        for (int z = 0; z < _depth; z++)
        {
            for (int x = 0; x < _width; x++)
            {
                _graph.CalculateConnections(x, z);
            }

            _counter += _width;

            if (_counter > _yieldEvery)
            {
                _counter = 0;
                yield return null;
            }
        }
    }
}
