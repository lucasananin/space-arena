//using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [Title("// General")]
    [SerializeField] Transform _parentContainer = null;

    [Title("// Weapons")]
    [SerializeField] bool _spawnEachOnStart = false;
    [SerializeField] bool _spawnRandomOnStart = false;
    [SerializeField] LootBehaviour _weaponLootPrefab = null;
    [SerializeField] BoxCollider2D _weaponCollider = null;
    [SerializeField] WeaponSO[] _weapons = null;
    [SerializeField, ReadOnly] List<WeaponSO> _droppedWeapons = null;

    [Title("// Collectables")]
    [SerializeField] Vector2 _minMaxForce = default;

    [Title("// Chests")]
    [SerializeField] LootDropper _chestPrefab = null;
    [SerializeField] BoxCollider2D _chestCollider = null;
    [SerializeField, ReadOnly] List<LootDropper> _chestsSpawned = null;

    //private GridGraph _graph = null;

    private void Start()
    {
        //_graph = AstarPath.active.data.graphs[0] as GridGraph;

        if (_spawnEachOnStart)
            SpawnEachWeaponOnList();

        if (_spawnRandomOnStart)
            SpawnRandomWeapon();
    }

    private void OnEnable()
    {
        EnemySpawner.OnEndWave += SpawnChest;
        EnemySpawner.OnStartWave += DestroyChests;
        EnemySpawner.OnEndWaveGroupChanged += DestroyChests;
    }

    private void OnDisable()
    {
        EnemySpawner.OnEndWave -= SpawnChest;
        EnemySpawner.OnStartWave -= DestroyChests;
        EnemySpawner.OnEndWaveGroupChanged -= DestroyChests;
    }

    [Button]
    private void SpawnRandomWeapon()
    {
        //GridNode[] _nodes = _graph.nodes;
        //int _randomNodeIndex = Random.Range(0, _nodes.Length);
        //Vector3 _position = (Vector3)_nodes[_randomNodeIndex].position;
        //LootBehaviour _loot = Instantiate(_weaponLootPrefab, _position, Quaternion.identity, _parentContainer);

        //Vector3 _randomPosition = Random.insideUnitCircle * 5f;
        //LootBehaviour _loot = Instantiate(_weaponLootPrefab, _randomPosition, Quaternion.identity, _parentContainer);

        var _position = GeneralMethods.GetRandomPointInBounds(_weaponCollider.bounds);
        var _loot = Instantiate(_weaponLootPrefab, _position, Quaternion.identity, _parentContainer);
        var _randomWeaponIndex = Random.Range(0, _weapons.Length);
        var _so = _weapons[_randomWeaponIndex];
        _loot.Init(_so);
    }

    //[Button]
    private void SpawnEachWeaponOnList()
    {
        int _count = _weapons.Length;

        for (int i = 0; i < _count; i++)
        {
            var _position = GeneralMethods.GetRandomPointInBounds(_weaponCollider.bounds);
            var _loot = Instantiate(_weaponLootPrefab, _position, Quaternion.identity, _parentContainer);
            var _so = _weapons[i];
            _loot.Init(_so);
        }
    }

    //[Button]
    private void SpawnChest(WaveModel _wave)
    {
        if (_wave is null) return;

        int _count = _wave.Loots.Length;

        for (int i = 0; i < _count; i++)
        {
            var _position = GetChestPosition(_count, i);
            var _chest = Instantiate(_chestPrefab, _position, Quaternion.identity, _parentContainer);
            _chest.SetLoot(_wave.Loots[i]);
            _chestsSpawned.Add(_chest);
        }
    }

    private Vector3 GetChestPosition(int _count, int _index)
    {
        var _indexCenter = _chestCollider.size.x / _count;
        var _x = _chestCollider.bounds.min.x + _indexCenter * ++_index - (_indexCenter / 2f);
        var _y = GeneralMethods.GetRandomPointInBounds(_chestCollider.bounds).y;
        return new Vector2(_x, _y);
    }

    private void DestroyChests()
    {
        DestroyChests(null);
    }

    private void DestroyChests(WaveModel _wave)
    {
        int _count = _chestsSpawned.Count;
        for (int i = _count - 1; i >= 0; i--)
            Destroy(_chestsSpawned[i].gameObject);
        _chestsSpawned.Clear();

        var _weapons = FindObjectsOfType<WeaponLoot>();
        _count = _weapons.Length;
        for (int i = _count - 1; i >= 0; i--)
            Destroy(_weapons[i].gameObject);
    }

    public void SpawnLoot(SpawnLootModel _lootSpawnInfo)
    {
        for (int i = 0; i < _lootSpawnInfo.quantity; i++)
        {
            switch (_lootSpawnInfo.so)
            {
                case CollectableSO:
                    var _collectableSO = _lootSpawnInfo.so as CollectableSO;
                    var _collectableInstance = Instantiate(_collectableSO.Prefab, _lootSpawnInfo.spawnPosition, Quaternion.identity, _parentContainer);

                    var _2DPosition = (Vector2)_lootSpawnInfo.spawnPosition;
                    var _randomPosition = Random.insideUnitCircle + _2DPosition;
                    var _direction = (_randomPosition - _2DPosition).normalized;
                    var _force = _direction * Random.Range(_minMaxForce.x, _minMaxForce.y);
                    var _rb = _collectableInstance.GetComponent<Rigidbody2D>();
                    _rb.AddForce(_force, ForceMode2D.Impulse);

                    break;
                case WeaponSO:
                    var _weaponSO = _lootSpawnInfo.so as WeaponSO;
                    var _weaponLootInstance = Instantiate(_weaponLootPrefab, _lootSpawnInfo.spawnPosition, Quaternion.identity, _parentContainer);
                    _weaponLootInstance.Init(_weaponSO);

                    if (!HasWeaponAlreadyBeenDropped(_weaponSO))
                    {
                        _droppedWeapons.Add(_weaponSO);
                    }

                    break;
                default:
                    Debug.LogError($"// This loot type SO is not available! {nameof(_lootSpawnInfo.so)}");
                    break;
            }
        }
    }

    public bool HasWeaponAlreadyBeenDropped(WeaponSO _value)
    {
        return _droppedWeapons.Count > 0 && _droppedWeapons.Contains(_value);
    }
}