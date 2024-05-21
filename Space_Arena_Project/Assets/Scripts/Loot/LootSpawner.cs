//using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] Transform _parentContainer = null;
    [SerializeField] Vector2 _minMaxForce = default;
    [Space]
    [SerializeField] WeaponSO[] _weaponSOs = null;
    [SerializeField] LootBehaviour _weaponLootPrefab = null;
    [SerializeField] CollectableBehaviour _ammoPrefab = null;
    [SerializeField] CollectableBehaviour _healthPrefab = null;
    [SerializeField] BoxCollider2D _boxCollider2D = null;
    [SerializeField] bool _spawnOnStart = false;

    //private GridGraph _graph = null;

    private void Start()
    {
        //_graph = AstarPath.active.data.graphs[0] as GridGraph;

        if (_spawnOnStart)
        {
            SpawnEachWeaponOnList();
        }
    }

    [Button]
    private void SpawnRandomWeapon()
    {
        Vector3 _position = GeneralMethods.RandomPointInBounds(_boxCollider2D.bounds);
        LootBehaviour _loot = Instantiate(_weaponLootPrefab, _position, Quaternion.identity, _parentContainer);

        //GridNode[] _nodes = _graph.nodes;
        //int _randomNodeIndex = Random.Range(0, _nodes.Length);
        //Vector3 _position = (Vector3)_nodes[_randomNodeIndex].position;
        //LootBehaviour _loot = Instantiate(_weaponLootPrefab, _position, Quaternion.identity, _parentContainer);

        //Vector3 _randomPosition = Random.insideUnitCircle * 5f;
        //LootBehaviour _loot = Instantiate(_weaponLootPrefab, _randomPosition, Quaternion.identity, _parentContainer);

        int _randomWeaponIndex = Random.Range(0, _weaponSOs.Length);
        WeaponSO _so = _weaponSOs[_randomWeaponIndex];
        _loot.Init(_so);
    }

    [Button]
    private void SpawnEachWeaponOnList()
    {
        int _count = _weaponSOs.Length;

        for (int i = 0; i < _count; i++)
        {
            Vector3 _position = GeneralMethods.RandomPointInBounds(_boxCollider2D.bounds);
            LootBehaviour _loot = Instantiate(_weaponLootPrefab, _position, Quaternion.identity, _parentContainer);

            WeaponSO _so = _weaponSOs[i];
            _loot.Init(_so);
        }
    }

    [Button]
    private void SpawnAmmoPack()
    {
        var _position = GeneralMethods.RandomPointInBounds(_boxCollider2D.bounds);
        var _randomEuler = new Vector3(0f, 0f, Random.rotation.eulerAngles.z);
        Instantiate(_ammoPrefab, _position, Quaternion.Euler(_randomEuler), _parentContainer);
        //var _loot = Instantiate(_ammoPrefab, _position, Quaternion.Euler(_randomEuler), _parentContainer);
        //var _randomIndex = Random.Range(0, _ammoSOs.Length);
        //var _so = _ammoSOs[_randomIndex];
        //_loot.Init(_so);
    }

    [Button]
    private void SpawnHealtPack()
    {
        var _position = GeneralMethods.RandomPointInBounds(_boxCollider2D.bounds);
        Instantiate(_healthPrefab, _position, Quaternion.identity, _parentContainer);
    }

    public void SpawnEntityLoot(Vector3 _spawnPoint, ScriptableObject _so, int _quantity)
    {
        for (int i = 0; i < _quantity; i++)
        {
            switch (_so)
            {
                case CollectableSO:
                    var _collectableSO = _so as CollectableSO;
                    var _instance = Instantiate(_collectableSO.Prefab, _spawnPoint, Quaternion.identity, _parentContainer);

                    var _2DPosition = (Vector2)_spawnPoint;
                    var _randomPosition = Random.insideUnitCircle + _2DPosition;
                    var _direction = (_randomPosition - _2DPosition).normalized;
                    var _force = _direction * Random.Range(_minMaxForce.x, _minMaxForce.y);
                    var _rb = _instance.GetComponent<Rigidbody2D>();
                    _rb.AddForce(_force, ForceMode2D.Impulse);

                    break;
                case WeaponSO:
                    break;
                default:
                    break;
            }
        }
    }
}