using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField] LootTableSO _lootTableSO = null;
    //[SerializeField] LootModelCollection[] _lootCollections = null;

    [Button]
    public void Roll()
    {
        var _lootCollection = _lootTableSO.GenerateLootCollection();
        int _count = _lootCollection.Models.Count;

        for (int i = 0; i < _count; i++)
        {
            var _model = _lootCollection.Models[i];

            var _spawnLootModel = new SpawnLootModel();
            _spawnLootModel.spawnPosition = transform.position;
            _spawnLootModel.so = _model.So;
            _spawnLootModel.quantity = _model.GetRandomQuantity();

            FindAnyObjectByType<LootSpawner>().SpawnEntityLoot(_spawnLootModel);
        }

        //int _count = _lootTableSO.LootCollections.Length;

        //for (int i = 0; i < _count; i++)
        //{
        //    var _collection = _lootTableSO.LootCollections[i];

        //    int _randomIndex = Random.Range(0, _collection.LootModels.Length);
        //    var _model = _collection.LootModels[_randomIndex];

        //    if (_model.So is null)
        //    {
        //        continue;
        //    }

        //    float _randomQuantity = Random.Range(_model.Quantity.x, _model.Quantity.y);
        //    int _quantity = Mathf.RoundToInt(_randomQuantity);

        //    FindAnyObjectByType<LootSpawner>().SpawnEntityLoot(transform.position, _model.So, _quantity);
        //}
    }
}

public class SpawnLootModel
{
    public Vector3 spawnPosition = default;
    public ScriptableObject so = null;
    public int quantity = 0;
}

//[System.Serializable]
//public class LootModelCollection
//{
//    [SerializeField] LootModel[] _lootModels = null;

//    public LootModel[] LootModels { get => _lootModels; set => _lootModels = value; }
//}

//[System.Serializable]
//public class LootModel
//{
//    [SerializeField] ScriptableObject _so = null;
//    [SerializeField] float _odd = 1f;
//    [SerializeField] Vector2 _quantity = Vector2.one;

//    public ScriptableObject So { get => _so; set => _so = value; }
//    public float Odd { get => _odd; set => _odd = value; }
//    public Vector2 Quantity { get => _quantity; set => _quantity = value; }
//}
