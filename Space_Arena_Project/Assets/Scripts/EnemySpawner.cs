using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveModel[] _waves = null;
    [SerializeField] Transform _container = null;
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
        _waveModel.ResetRuntimeQuantities();

        _activeSpawnCount = 0;
        int _totalSpawnedCount = 0;
        int _totalSpawnQuantity = _waveModel.GetTotalQuantity();

        onStart?.Invoke();

        do
        {
            while (_activeSpawnCount >= _waveModel.MaxActiveSpawns)
            {
                yield return null;
            }

            _activeSpawnCount++;
            _totalSpawnedCount++;

            AiEntity _prefab = _waveModel.GetEntityPrefab();
            Vector3 _position = GetRandomNodePosition();
            Instantiate(_prefab, _position, Quaternion.identity, _container);

            yield return new WaitForSeconds(_spawnTime);

        } while (_totalSpawnedCount < _totalSpawnQuantity);
        //} while (_totalSpawnedCount < _waveModel.MaxSpawnedCount);

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
    [SerializeField] EntityGroup[] _entities = null;
    //[SerializeField] AiEntitySO[] _availableEntities = null;
    //[SerializeField, Range(0, 20)] int _maxSpawnedCount = 12;
    [SerializeField, Range(0, 20)] int _maxActiveSpawns = 4;
    [SerializeField, ReadOnly] List<float> _runtimeQuantities = null;

    //public int MaxSpawnedCount { get => _maxSpawnedCount; private set => _maxSpawnedCount = value; }
    public int MaxActiveSpawns { get => _maxActiveSpawns; private set => _maxActiveSpawns = value; }

    public AiEntity GetEntityPrefab()
    {
        //int _randomIndex = Random.Range(0, _availableEntities.Length);
        //return _availableEntities[_randomIndex].EntityPrefab;

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

    [Button]
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

    //[Button]
    //public void ClearQuantities()
    //{
    //    _quantities.Clear();
    //}
}

[System.Serializable]
public class EntityGroup
{
    [SerializeField] AiEntitySO _so = null;
    [SerializeField, Range(0, 99)] int _quantity = 1;

    public AiEntitySO SO { get => _so; set => _so = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }
}
