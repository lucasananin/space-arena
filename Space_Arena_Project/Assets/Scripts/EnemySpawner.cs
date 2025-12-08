using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] RandomizedWaveModel[] _randomizedWaves = null;
    [SerializeField] bool _useRandomizedWaves = false;
    [Space]
    [SerializeField] Transform _container = null;
    [SerializeField] List<WaveSO> _waveGroups = null;
    [Space]
    [SerializeField, ReadOnly] PlayerEntity _player = null;
    [SerializeField, ReadOnly] WaveSO _waveGroup = null;
    [SerializeField, ReadOnly] WaveModel _waveModel = null;
    [SerializeField, ReadOnly] int _groupIndex = 0;
    [SerializeField, ReadOnly] int _waveIndex = 0;
    [SerializeField, ReadOnly] bool _isSpawning = false;
    [SerializeField, ReadOnly] int _activeSpawnCount = 0;

    [Header("// EVENTS")]
    [SerializeField] UnityEvent OnWaveEnd_Event = null;

    private GridGraph _graph = null;

    public static event System.Action OnStartWaveGroupChanged = null;
    public static event System.Action<WaveSO> OnEndWaveGroupChanged = null;
    public static event System.Action<WaveModel> OnStartWave = null;
    public static event System.Action<WaveModel> OnEndWave = null;
    public static event System.Action<WaveModel> OnEndFinalWave = null;

    private void Start()
    {
        _graph = AstarPath.active.data.graphs[0] as GridGraph;
        _player = FindAnyObjectByType<PlayerEntity>();
        _waveGroup = _waveGroups[_groupIndex];
    }

    private void OnEnable()
    {
        EnemyHealth.OnAnyAiDead += DecreaseActiveSpawnCount;
        StartRoundInteractable.OnInteracted += SpawnEnemies;
    }

    private void OnDisable()
    {
        EnemyHealth.OnAnyAiDead -= DecreaseActiveSpawnCount;
        StartRoundInteractable.OnInteracted -= SpawnEnemies;
    }

    [Button]
    private void SpawnEnemies()
    {
        if (_isSpawning) return;

        if (_waveIndex >= _waveGroup.Waves.Length)
        {
            StartCoroutine(ChangeWaveGroup_routine());
        }
        else
        {
            StartCoroutine(Spawn_routine());
        }
    }

    private IEnumerator ChangeWaveGroup_routine()
    {
        OnStartWaveGroupChanged?.Invoke();

        yield return null;

        _waveGroup = _waveGroups[_groupIndex];
        ResetWaveIndex();
        OnEndWaveGroupChanged?.Invoke(_waveGroup);
        StartCoroutine(Spawn_routine());
    }

    private IEnumerator Spawn_routine()
    {
        _isSpawning = true;
        _waveModel = _waveGroup.Waves[_waveIndex];
        _waveModel.ResetRuntimeQuantities();
        _activeSpawnCount = 0;
        int _totalSpawnedCount = 0;
        int _totalSpawnQuantity = _useRandomizedWaves ? _randomizedWaves[0].Quantity : _waveModel.GetTotalQuantity();
        OnStartWave?.Invoke(_waveModel);

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

            yield return new WaitForSeconds(_waveGroup.SpawnTime);

        } while (_totalSpawnedCount < _totalSpawnQuantity);

        while (_activeSpawnCount > 0)
        {
            yield return null;
        }

        _isSpawning = false;
        _waveIndex++;
        OnEndWave?.Invoke(_waveModel);
        OnWaveEnd_Event?.Invoke();
        CheckIfIsLastWaveGroup();
    }

    private void CheckIfIsLastWaveGroup()
    {
        bool _isLastWave = _waveIndex >= _waveGroup.Waves.Length;

        if (!_isLastWave) return;

        _groupIndex++;
        bool _isLastGroup = _groupIndex >= _waveGroups.Count;

        if (!_isLastGroup) return;

        OnEndFinalWave?.Invoke(_waveModel);
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

            var _isCloseToPlayer = GeneralMethods.IsPointCloseToTarget(_finalPosition, _playerPosition, _waveGroup.DistanceRange.x);
            var _isFarFromPlayer = !GeneralMethods.IsPointCloseToTarget(_finalPosition, _playerPosition, _waveGroup.DistanceRange.y);

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

    public int GetTotalWaveCount()
    {
        int _total = 0;
        int _count_1 = _waveGroups.Count;

        for (int i = 0; i < _count_1; i++)
        {
            int _count_2 = _waveGroups[i].Waves.Length;

            for (int j = 0; j < _count_2; j++)
            {
                _total++;
            }

            _total++;
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
