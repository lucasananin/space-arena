using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave_", menuName = "SO/Wave")]
public class WaveSO : ScriptableObject
{
    [SerializeField] GameObject _environment = null;
    [SerializeField] Vector2 _distanceRange = new(6f, 12f);
    [SerializeField, Range(0.1f, 9f)] float _spawnTime = 0.4f;
    [SerializeField] WaveModel[] _waves = null;

    public WaveModel[] Waves { get => _waves; private set => _waves = value; }
    public Vector2 DistanceRange { get => _distanceRange; private set => _distanceRange = value; }
    public float SpawnTime { get => _spawnTime; private set => _spawnTime = value; }
    public GameObject Environment { get => _environment; private set => _environment = value; }
}

[System.Serializable]
public class WaveModel
{
    [Title("- WAVE -", null, TitleAlignments.Centered)]
    [SerializeField] EntityGroup[] _entities = null;
    [SerializeField] LootTableSO[] _loots = null;
    [SerializeField, Range(1, 20)] int _maxActiveSpawns = 4;

    readonly List<float> _runtimeQuantities = new();

    public LootTableSO[] Loots { get => _loots; set => _loots = value; }
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
public class EntityGroup
{
    [SerializeField] AiEntitySO _so = null;
    [SerializeField, Range(0, 99)] int _quantity = 1;

    public AiEntitySO SO { get => _so; set => _so = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }
}
