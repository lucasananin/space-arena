using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveModel[] _waves = null;
    [SerializeField] RandomizedWaveModel[] _randomizedWaves = null;
    [SerializeField] Transform _container = null;
    [SerializeField, Range(0.1f, 9f)] float _spawnTime = 1f;
    [SerializeField, Range(0f, 12f)] float _minDistanceFromPlayer = 6f;
    [SerializeField] bool _useRandomizedWaves = false;
    [Space]
    [SerializeField, ReadOnly] WaveModel _waveModel = null;
    [SerializeField, ReadOnly] PlayerEntity _player = null;
    [SerializeField, ReadOnly] int _waveIndex = 0;
    [SerializeField, ReadOnly] bool _isSpawning = false;
    [SerializeField, ReadOnly] int _activeSpawnCount = 0;

    private GridGraph _graph = null;

    public static event System.Action onStart = null;
    public static event System.Action onEnd = null;

    private void Start()
    {
        _graph = AstarPath.active.data.graphs[0] as GridGraph;
        _player = FindAnyObjectByType<PlayerEntity>();
    }

    private void OnEnable()
    {
        EnemyHealth.OnAnyAiDead += DecreaseActiveSpawnCount;
        StartRoundInteractable.onInteracted += SpawnEnemies;
    }

    private void OnDisable()
    {
        EnemyHealth.OnAnyAiDead -= DecreaseActiveSpawnCount;
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
        _waveModel.ResetRuntimeQuantities();
        _activeSpawnCount = 0;
        int _totalSpawnedCount = 0;
        int _totalSpawnQuantity = _useRandomizedWaves ? _randomizedWaves[0].Quantity : _waveModel.GetTotalQuantity();
        onStart?.Invoke();

        do
        {
            var _maxActiveSpawns = _useRandomizedWaves ? _randomizedWaves[0].MaxActiveSpawns : _waveModel.MaxActiveSpawns;
            while (_activeSpawnCount >= _maxActiveSpawns)
            {
                yield return null;
            }

            _activeSpawnCount++;
            _totalSpawnedCount++;
            AiEntity _prefab = _useRandomizedWaves ? _randomizedWaves[0].GetEntityPrefab() : _waveModel.GetEntityPrefab();
            Vector3 _position = GetRandomNodePosition();
            Instantiate(_prefab, _position, Quaternion.identity, _container);

            yield return new WaitForSeconds(_spawnTime);

        } while (_totalSpawnedCount < _totalSpawnQuantity);

        while (_activeSpawnCount > 0)
        {
            yield return null;
        }

        _isSpawning = false;
        _waveIndex++;
        onEnd?.Invoke();

        if (_waveIndex >= _waves.Length)
        {
            ResetWaveIndex();
        }
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

            var _isCloseToPlayer = GeneralMethods.IsPointCloseToTarget(_position, _player.transform.position, _minDistanceFromPlayer);

            if (_isCloseToPlayer)
            {
                _position = Vector3.zero;
            }

        } while (_position == Vector3.zero);

        return _position;
    }

    private void DecreaseActiveSpawnCount(HealthBehaviour _obj)
    {
        _activeSpawnCount--;
    }

    [Button]
    private void ResetWaveIndex()
    {
        _waveIndex = 0;
    }
}

[System.Serializable]
public class WaveModel
{
    [SerializeField] EntityGroup[] _entities = null;
    [SerializeField, Range(0, 20)] int _maxActiveSpawns = 4;
    [SerializeField, ReadOnly] List<float> _runtimeQuantities = null;

    public int MaxActiveSpawns { get => _maxActiveSpawns; private set => _maxActiveSpawns = value; }

    public AiEntity GetEntityPrefab()
    {
        bool _canSpawn;
        int _randomIndex;

        do
        {
            _randomIndex = Random.Range(0, _entities.Length);
            _canSpawn = _runtimeQuantities[_randomIndex] > 0;

        } while (!_canSpawn);

        _runtimeQuantities[_randomIndex]--;
        return _entities[_randomIndex].SO.EntityPrefab;
    }

    public void ResetRuntimeQuantities()
    {
        _runtimeQuantities.Clear();

        int _count = _entities.Length;

        for (int i = 0; i < _count; i++)
        {
            _runtimeQuantities.Add(_entities[i].Quantity);
        }
    }

    public int GetTotalQuantity()
    {
        int _count = _entities.Length;
        int _total = 0;

        for (int i = 0; i < _count; i++)
        {
            _total += _entities[i].Quantity;
        }

        return _total;
    }
}

[System.Serializable]
public class RandomizedWaveModel
{
    [SerializeField] EntityGroup[] _entities = null;
    [SerializeField, Range(0, 20)] int _quantity = 4;
    [SerializeField, Range(0, 20)] int _maxActiveSpawns = 4;

    public int Quantity { get => _quantity; private set => _quantity = value; }
    public int MaxActiveSpawns { get => _maxActiveSpawns; private set => _maxActiveSpawns = value; }

    public AiEntity GetEntityPrefab()
    {
        var _randomIndex = Random.Range(0, _entities.Length);
        return _entities[_randomIndex].SO.EntityPrefab;
    }
}

[System.Serializable]
public class EntityGroup
{
    [SerializeField] AiEntitySO _so = null;
    [SerializeField, Range(0, 99)] int _quantity = 1;

    public AiEntitySO SO { get => _so; set => _so = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }
}
