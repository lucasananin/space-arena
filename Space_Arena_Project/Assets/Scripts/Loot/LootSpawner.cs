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

    [Title("// Collectables")]
    [SerializeField] CollectableBehaviour _ammoPrefab = null;
    [SerializeField] CollectableBehaviour _healthPrefab = null;
    [SerializeField] Vector2 _minMaxForce = default;

    [Title("// Chests")]
    [SerializeField] WeaponChestCollidable _chestPrefab = null;
    [SerializeField] BoxCollider2D _chestCollider = null;
    [SerializeField, ReadOnly] WeaponChestCollidable _currentChest = null;

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
        EnemySpawner.onEnd += SpawnChest;
        EnemySpawner.onStart += DestroyChest;
    }

    private void OnDisable()
    {
        EnemySpawner.onEnd -= SpawnChest;
        EnemySpawner.onStart -= DestroyChest;
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

    [Button]
    private void SpawnChest()
    {
        var _position = GeneralMethods.GetRandomPointInBounds(_chestCollider.bounds);
        var _chest = Instantiate(_chestPrefab, _position, Quaternion.identity, _parentContainer);
        _currentChest = _chest;
    }

    private void DestroyChest()
    {
        if (_currentChest is not null)
            Destroy(_currentChest.gameObject);
    }

    //[Button]
    private void SpawnAmmoPack()
    {
        var _position = GeneralMethods.GetRandomPointInBounds(_weaponCollider.bounds);
        var _randomEuler = new Vector3(0f, 0f, Random.rotation.eulerAngles.z);
        Instantiate(_ammoPrefab, _position, Quaternion.Euler(_randomEuler), _parentContainer);
        //var _loot = Instantiate(_ammoPrefab, _position, Quaternion.Euler(_randomEuler), _parentContainer);
        //var _randomIndex = Random.Range(0, _ammoSOs.Length);
        //var _so = _ammoSOs[_randomIndex];
        //_loot.Init(_so);
    }

    //[Button]
    private void SpawnHealtPack()
    {
        var _position = GeneralMethods.GetRandomPointInBounds(_weaponCollider.bounds);
        Instantiate(_healthPrefab, _position, Quaternion.identity, _parentContainer);
    }

    public void SpawnEntityLoot(SpawnLootModel _lootSpawnInfo)
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

                    break;
                default:
                    Debug.LogError($"// This loot type SO is not available! {nameof(_lootSpawnInfo.so)}");
                    break;
            }
        }
    }
}