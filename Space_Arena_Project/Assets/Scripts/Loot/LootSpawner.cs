using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] WeaponSO[] _weaponSOs = null;
    [SerializeField] LootBehaviour _weaponLootPrefab = null;
    [SerializeField] Transform _parentContainer = null;
    [SerializeField] BoxCollider2D _boxCollider2D = null;

    //private GridGraph _graph = null;

    private void Start()
    {
        //_graph = AstarPath.active.data.graphs[0] as GridGraph;
        SpawnEachOnList();
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
    private void SpawnEachOnList()
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
}

[System.Serializable]
public class LootModel
{
    [SerializeField] ScriptableObject _so = null;
    [SerializeField, Range(0f, 99f)] float _dropChance = 0f;

    public ScriptableObject So { get => _so; set => _so = value; }
    public float DropChance { get => _dropChance; set => _dropChance = value; }
}