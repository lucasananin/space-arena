using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo_", menuName = "SO/Combat/Ammo Type")]
public class AmmoSO : ScriptableObject
{
    [SerializeField] string _id = null;
    //[SerializeField] string _displayName = null;
    [SerializeField] int _maxQuantity = 120;

    public string Id { get => _id; private set => _id = value; }
    public int MaxQuantity { get => _maxQuantity; private set => _maxQuantity = value; }
}

[System.Serializable]
public class AmmoModel
{
    [SerializeField] AmmoSO _so = null;
    [SerializeField, ReadOnly] int _quantity = 0;

    public void DecreaseQuantity(int _value)
    {
        _quantity -= _value;

        if (_quantity < 0)
            _quantity = 0;
    }

    public void RestoreQuantity()
    {
        _quantity = _so.MaxQuantity;
    }

    public bool HasEnoughQuantity(int _value)
    {
        return _quantity >= _value;
    }

    public string GetId()
    {
        return _so.Id;
    }
}