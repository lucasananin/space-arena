using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField] LootTableSO _lootTableSO = null;
    [SerializeField] LootModelCollection[] _lootCollections = null;

    [Button]
    public void Roll()
    {
        int _count = _lootTableSO.LootCollections.Length;

        for (int i = 0; i < _count; i++)
        {
            var _collection = _lootTableSO.LootCollections[i];

            int _randomIndex = Random.Range(0, _collection.LootModels.Length);
            var _model = _collection.LootModels[_randomIndex];

            if (_model.So is null)
            {
                continue;
            }

            float _randomQuantity = Random.Range(_model.Quantity.x, _model.Quantity.y);
            int _quantity = Mathf.RoundToInt(_randomQuantity);

            FindAnyObjectByType<LootSpawner>().SpawnEntityLoot(transform.position, _model.So, _quantity);
        }
    }
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
