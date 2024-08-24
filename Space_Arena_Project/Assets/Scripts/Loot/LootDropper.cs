using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
    [SerializeField] LootTableSO _lootTableSO = null;

    [Button]
    public virtual void Drop()
    {
        Drop(transform.position);
    }

    public virtual void Drop(Vector3 _origin)
    {
        var _lootCollection = _lootTableSO.GenerateLootCollection();
        int _count = _lootCollection.Models.Count;

        for (int i = 0; i < _count; i++)
        {
            var _model = _lootCollection.Models[i];

            var _spawnLootModel = new SpawnLootModel();
            _spawnLootModel.spawnPosition = _origin;
            _spawnLootModel.so = _model.So;
            _spawnLootModel.quantity = _model.GetRandomQuantity();

            FindAnyObjectByType<LootSpawner>().SpawnEntityLoot(_spawnLootModel);
        }
    }

    public void SetLoot(LootTableSO _lootTable)
    {
        _lootTableSO = _lootTable;
    }
}

public class SpawnLootModel
{
    public Vector3 spawnPosition = default;
    public ScriptableObject so = null;
    public int quantity = 0;
}
