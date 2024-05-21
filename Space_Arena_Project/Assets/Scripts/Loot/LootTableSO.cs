using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable_", menuName = "SO/Loot Table")]
public class LootTableSO : ScriptableObject
{
    [SerializeField] LootModelCollection[] _lootCollections = null;

    public LootModelCollection[] LootCollections { get => _lootCollections; private set => _lootCollections = value; }

    public LootModelCollection GenerateLootCollection()
    {
        var _newCollection = new LootModelCollection();
        int _count = _lootCollections.Length;

        for (int i = 0; i < _count; i++)
        {
            var _collection = _lootCollections[i];

            var _randomIndex = Random.Range(0, _collection.Models.Count);
            var _model = _collection.Models[_randomIndex];

            if (_model.So is not null)
            {
                _newCollection.Models.Add(_model);
            }
        }

        return _newCollection;
    }

    // GetListOfOdds(LootModelCollection _collection) { }
}

[System.Serializable]
public class LootModelCollection
{
    [SerializeField] List<LootModel> _models = new List<LootModel>();

    public List<LootModel> Models { get => _models; set => _models = value; }
}

[System.Serializable]
public class LootModel
{
    [SerializeField] ScriptableObject _so = null;
    [SerializeField] float _odd = 1f;
    [SerializeField] Vector2 _minMaxQuantity = Vector2.one;

    public ScriptableObject So { get => _so; set => _so = value; }
    public float Odd { get => _odd; set => _odd = value; }
    public Vector2 Quantity { get => _minMaxQuantity; set => _minMaxQuantity = value; }

    public int GetRandomQuantity()
    {
        float _randomQuantity = Random.Range(_minMaxQuantity.x, _minMaxQuantity.y);
        return Mathf.RoundToInt(_randomQuantity);
    }
}

//public class LootSpawnInfo
//{
//    public Vector3 spawnPosition = default;
//    public ScriptableObject so = null;
//    public int quantity = 0;
//}