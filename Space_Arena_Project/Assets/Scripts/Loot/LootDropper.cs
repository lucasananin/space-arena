using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
    [SerializeField] LootTableSO _lootTableSO = null;
    [SerializeField, ReadOnly] LootSpawner _lootSpawner = null;

    private const int MAX_ATTEMPTS = 10;

    private void Start()
    {
        _lootSpawner = FindAnyObjectByType<LootSpawner>();
    }

    [Button]
    public virtual void Drop()
    {
        Drop(transform.position);
    }

    public virtual void Drop(Vector3 _origin)
    {
        var _lootCollection = GenerateLootCollection();
        int _count = _lootCollection.Models.Count;

        for (int i = 0; i < _count; i++)
        {
            var _model = _lootCollection.Models[i];

            var _spawnLootModel = new SpawnLootModel
            {
                spawnPosition = _origin,
                so = _model.So,
                quantity = _model.GetRandomQuantity()
            };

            _lootSpawner.SpawnLoot(_spawnLootModel);
        }
    }

    private LootModelCollection GenerateLootCollection()
    {
        LootModelCollection _finalCollection = new();

        for (int i = 0; i < MAX_ATTEMPTS; i++)
        {
            _finalCollection = _lootTableSO.GenerateLootCollection();

            var _count = _finalCollection.Models.Count;
            var _hasRepeatedWeapon = false;

            for (int j = 0; j < _count; j++)
            {
                var _model = _finalCollection.Models[j];

                if (_model.So is not WeaponSO) continue;
                if (_lootSpawner.HasWeaponAlreadyBeenDropped(_model.So as WeaponSO))
                {
                    _hasRepeatedWeapon = true;
                    break;
                }
            }

            if (!_hasRepeatedWeapon)
            {
                break;
            }
        }

        return _finalCollection;
    }

    public void SetLoot(LootTableSO _lootTable)
    {
        _lootTableSO = _lootTable;
    }
}

public class SpawnLootModel
{
    public ScriptableObject so = null;
    public Vector3 spawnPosition = default;
    public int quantity = 0;
}
