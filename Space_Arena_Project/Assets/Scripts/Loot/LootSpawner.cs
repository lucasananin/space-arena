using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] WeaponSO[] _weaponSOs = null;
    [SerializeField] LootBehaviour _weaponLootPrefab = null;

    private void Start()
    {
        SpawnWeaponLoot();
    }

    [Button]
    private void SpawnWeaponLoot()
    {
        Vector3 _randomPosition = Random.insideUnitCircle * 5f;
        LootBehaviour _loot = Instantiate(_weaponLootPrefab, _randomPosition, Quaternion.identity);

        int _randomIndex = Random.Range(0, _weaponSOs.Length);
        WeaponSO _so = _weaponSOs[_randomIndex];
        _loot.Init(_so);
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