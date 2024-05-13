using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveModel[] _waves = null;
    //[SerializeField] AiEntitySO[] _entitySOs = null;
    [SerializeField] Transform _container = null;
    //[SerializeField, Range(0, 20)] int _maxSpawnedCount = 12;
    //[SerializeField, Range(0, 20)] int _maxActiveSpawnCount = 4;
    [SerializeField, Range(0.1f, 9f)] float _spawnTime = 1f;
    [Space]
    [SerializeField, ReadOnly] WaveModel _waveModel = null;
    [SerializeField, ReadOnly] int _waveIndex = 0;
    [SerializeField, ReadOnly] bool _isSpawning = false;
    [SerializeField, ReadOnly] int _activeSpawnCount = 0;

    private GridGraph _graph = null;

    public static event System.Action onStart = null;
    public static event System.Action onEnd = null;

    private void Start()
    {
        _graph = AstarPath.active.data.graphs[0] as GridGraph;
    }

    private void OnEnable()
    {
        EnemyHealth.onAnyAiDead += DecreaseActiveSpawnCount;
        StartRoundInteractable.onInteracted += SpawnEnemies;
    }

    private void OnDisable()
    {
        EnemyHealth.onAnyAiDead -= DecreaseActiveSpawnCount;
        StartRoundInteractable.onInteracted -= SpawnEnemies;
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
        _waveModel = _waves[_waveIndex];
        _activeSpawnCount = 0;
        int _totalSpawnedCount = 0;
        onStart?.Invoke();

        do
        {
            while (_activeSpawnCount >= _waveModel.MaxActiveSpawnCount)
            {
                yield return null;
            }

            _activeSpawnCount++;
            _totalSpawnedCount++;

            int _randomIndex = Random.Range(0, _waveModel.EntitySOs.Length);
            var _prefab = _waveModel.EntitySOs[_randomIndex].EntityPrefab;
            Vector3 _position = GetRandomNodePosition();
            Instantiate(_prefab, _position, Quaternion.identity, _container);

            yield return new WaitForSeconds(_spawnTime);

        } while (_totalSpawnedCount < _waveModel.MaxSpawnedCount);

        while (_activeSpawnCount > 0)
        {
            yield return null;
        }

        _isSpawning = false;
        _waveIndex++;
        onEnd?.Invoke();
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

    private void DecreaseActiveSpawnCount(HealthBehaviour _obj)
    {
        _activeSpawnCount--;
    }
}

[System.Serializable]
public class WaveModel
{
    [SerializeField] AiEntitySO[] _entitySOs = null;
    [SerializeField, Range(0, 20)] int _maxSpawnedCount = 12;
    [SerializeField, Range(0, 20)] int _maxActiveSpawnCount = 4;

    public AiEntitySO[] EntitySOs { get => _entitySOs; private set => _entitySOs = value; }
    public int MaxSpawnedCount { get => _maxSpawnedCount; private set => _maxSpawnedCount = value; }
    public int MaxActiveSpawnCount { get => _maxActiveSpawnCount; private set => _maxActiveSpawnCount = value; }
}