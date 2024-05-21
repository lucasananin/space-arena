using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable_", menuName = "SO/Loot Table")]
public class LootTableSO : ScriptableObject
{
    [SerializeField] LootModelCollection[] _lootCollections = null;

    public LootModelCollection[] LootCollections { get => _lootCollections; private set => _lootCollections = value; }
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

public class LootSpawnInfo
{
    public Vector3 spawnPosition = default;
    public ScriptableObject so = null;
    public int quantity = 0;
}