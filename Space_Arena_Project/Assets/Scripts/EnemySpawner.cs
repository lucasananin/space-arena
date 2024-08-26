using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField] WaveModel[] _waves = null;
    [SerializeField] RandomizedWaveModel[] _randomizedWaves = null;
    [SerializeField] bool _useRandomizedWaves = false;
    [Space]
    [SerializeField] Transform _container = null;
    [SerializeField] WaveSO _waveSO = null;
    [Space]
    [SerializeField, ReadOnly] WaveModel _currentWave = null;
    [SerializeField, ReadOnly] PlayerEntity _player = null;
    [SerializeField, ReadOnly] int _waveIndex = 0;
    [SerializeField, ReadOnly] bool _isSpawning = false;
    [SerializeField, ReadOnly] int _activeSpawnCount = 0;

    private GridGraph _graph = null;

    public static event System.Action<WaveModel> OnStart = null;
    public static event System.Action<WaveModel> OnEnd = null;

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
        if (_waveIndex >= _waveSO.Waves.Length) ResetWaveIndex();

        _isSpawning = true;
        _currentWave = _waveSO.Waves[_waveIndex];
        _currentWave.ResetRuntimeQuantities();
        _activeSpawnCount = 0;
        int _totalSpawnedCount = 0;
        int _totalSpawnQuantity = _useRandomizedWaves ? _randomizedWaves[0].Quantity : _currentWave.GetTotalQuantity();
        OnStart?.Invoke(_currentWave);

        do
        {
            var _maxActiveSpawns = _useRandomizedWaves ? _randomizedWaves[0].MaxActiveSpawns : _currentWave.MaxActiveSpawns;
            while (_activeSpawnCount >= _maxActiveSpawns)
            {
                yield return null;
            }

            _activeSpawnCount++;
            _totalSpawnedCount++;
            AiEntity _prefab = _useRandomizedWaves ? _randomizedWaves[0].GetEntityPrefab() : _currentWave.GetEntityPrefab();
            Vector3 _position = GetRandomNodePosition();
            Instantiate(_prefab, _position, Quaternion.identity, _container);

            yield return new WaitForSeconds(_currentWave.SpawnTime);

        } while (_totalSpawnedCount < _totalSpawnQuantity);

        while (_activeSpawnCount > 0)
        {
            yield return null;
        }

        _isSpawning = false;
        _waveIndex++;
        OnEnd?.Invoke(_currentWave);
    }

    private Vector3 GetRandomNodePosition()
    {
        var _finalPosition = Vector3.zero;
        var _playerPosition = _player.transform.position;

        do
        {
            GridNode[] _nodes = _graph.nodes;
            int _randomIndex = Random.Range(0, _nodes.Length);
            var _node = _graph.nodes[_randomIndex];

            if (_node.Walkable)
            {
                _finalPosition = (Vector3)_node.position;
            }

            var _isCloseToPlayer = GeneralMethods.IsPointCloseToTarget(_finalPosition, _playerPosition, _currentWave.DistanceRange.x);
            var _isFarFromPlayer = !GeneralMethods.IsPointCloseToTarget(_finalPosition, _playerPosition, _currentWave.DistanceRange.y);

            if (_isCloseToPlayer || _isFarFromPlayer)
            {
                _finalPosition = Vector3.zero;
            }

        } while (_finalPosition == Vector3.zero);

        return _finalPosition;
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
