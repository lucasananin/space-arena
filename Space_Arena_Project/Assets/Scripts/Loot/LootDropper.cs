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
        //var _lootCollection = _lootTableSO.GenerateLootCollection();
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

            _lootSpawner.SpawnLoot(this, _spawnLootModel);
        }
    }

    private LootModelCollection GenerateLootCollection()
    {
        //bool _hasRepetition = false;
        LootModelCollection _finalCollection = new();
        //int _count;

        for (int i = 0; i < MAX_ATTEMPTS; i++)
        {
            _finalCollection = _lootTableSO.GenerateLootCollection();
            int _count = _finalCollection.Models.Count;
            bool _hasRepeatedWeapon = false;

            for (int j = 0; j < _count; j++)
            {
                var _model = _finalCollection.Models[j];

                if (_lootSpawner.HasWeaponAlreadyBeenDropped(_model.So as WeaponSO))
                {
                    Debug.Log($"// {i}");
                    _hasRepeatedWeapon = true;
                    break;
                }
            }

            if (!_hasRepeatedWeapon)
            {
                break;
            }
        }

        //do
        //{
        //    _finalCollection = _lootTableSO.GenerateLootCollection();
        //    _count = _finalCollection.Models.Count;

        //    for (int i = 0; i < _count; i++)
        //    {
        //        var _model = _finalCollection.Models[i];

        //        if (_lootSpawner.HasWeaponAlreadyBeenDropped(_model.So as WeaponSO))
        //        {
        //            _hasRepetition = true;
        //            break;
        //        }
        //        else
        //        {
        //            _hasRepetition = false;
        //        }
        //    }

        //} while (_hasRepetition);

        return _finalCollection;
    }

    //public virtual void ReDrop(WeaponSO _weaponSO, SpawnLootModel _spawnLootModel)
    //{
    //    do
    //    {
    //        var _lootCollection = _lootTableSO.GenerateLootCollection();
    //        int _count = _lootCollection.Models.Count;

    //    } while (true);
    //}

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
