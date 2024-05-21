using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    //[SerializeField] Transform _spawnPoint = null;
    [SerializeField] LootModelCollection[] _lootCollections = null;

    [Button]
    public void DropLoot()
    {
        int _count = _lootCollections.Length;

        for (int i = 0; i < _count; i++)
        {
            var _collection = _lootCollections[i];

            int _randomIndex = Random.Range(0, _collection.LootModels.Length);
            var _model = _collection.LootModels[_randomIndex];

            if (_model.So is null)
            {
                //Debug.Log($"// Drop null!");
                continue;
            }

            float _randomQuantity = Random.Range(_model.Quantity.x, _model.Quantity.y);
            int _quantity = Mathf.RoundToInt(_randomQuantity);

            FindAnyObjectByType<LootSpawner>().SpawnEntityLoot(transform.position, _model.So, _quantity);

            //Debug.Log($"// float = {_randomQuantity}, int = {_quantity}");
            //int _quantity = (int)_randomQuantity;
            //int _quantity = Mathf.CeilToInt(_randomQuantity);

            //for (int j = 0; j < _quantity; j++)
            //{
            //    switch (_model.So)
            //    {
            //        case CollectableSO:
            //            //var _collectableSO = _model.So as CollectableSO;
            //            //var _position = Random.insideUnitCircle + (Vector2)_spawnPoint.position;
            //            //Instantiate(_collectableSO.Prefab, _position, Quaternion.identity);
            //            break;
            //        case WeaponSO:
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //Debug.Log($"// Drop {_quantity} {_model.So.name}!");
        }
    }
}

[System.Serializable]
public class LootModelCollection
{
    [SerializeField] LootModel[] _lootModels = null;

    public LootModel[] LootModels { get => _lootModels; set => _lootModels = value; }
}

[System.Serializable]
public class LootModel
{
    [SerializeField] ScriptableObject _so = null;
    [SerializeField] float _odd = 1f;
    [SerializeField] Vector2 _quantity = Vector2.one;

    public ScriptableObject So { get => _so; set => _so = value; }
    public float Odd { get => _odd; set => _odd = value; }
    public Vector2 Quantity { get => _quantity; set => _quantity = value; }
}
