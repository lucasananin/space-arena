using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] AiEntitySO[] _entitySOs = null;
    [SerializeField] Transform _container = null;
    [SerializeField, Range(0, 20)] int _spawnCount = 12;
    //[SerializeField, Range(0, 20)] int _maxActiveSpawns = 4;
    [Space]
    [SerializeField, ReadOnly] bool _isSpawning = false;

    private GridGraph _graph = null;

    private void Start()
    {
        _graph = AstarPath.active.data.graphs[0] as GridGraph;
    }

    [Button]
    private void SpawnEnemies()
    {
        if (_isSpawning) return;

        StartCoroutine(Spawn_routine());
    }

    private IEnumerator Spawn_routine()
    {
        _isSpawning = true;

        int _currentSpawns = 0;

        do
        {
            // é preciso saber quando os inimigos morrem para poder travar o numero de instancias ativas.

            //while (_currentSpawns >= _maxActiveSpawns)
            //{
            //    yield return null;
            //}

            yield return new WaitForSeconds(1);

            _currentSpawns++;

            int _randomIndex = Random.Range(0, _entitySOs.Length);
            var _prefab = _entitySOs[_randomIndex].EntityPrefab;
            Vector3 _position = GetRandomNodePosition();
            Instantiate(_prefab, _position, Quaternion.identity, _container);

        } while (_currentSpawns < _spawnCount);

        _isSpawning = false;
    }

    private Vector3 GetRandomNodePosition()
    {
        Vector3 _position = Vector3.zero;

        do
        {
            GridNode[] _nodes = _graph.nodes;
            int _randomIndex = Random.Range(0, _nodes.Length);
            var _node = _graph.nodes[_randomIndex];

            if (_node.Walkable)
            {
                _position = (Vector3)_node.position;
            }

        } while (_position == Vector3.zero);

        return _position;
    }
}
