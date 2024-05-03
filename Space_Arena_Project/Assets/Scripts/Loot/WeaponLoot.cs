using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoot : LootBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer = null;
    [SerializeField, ReadOnly] WeaponSO _weaponSO = null;

    public WeaponSO WeaponSO { get => _weaponSO; private set => _weaponSO = value; }

    public override void Init(ScriptableObject _value)
    {
        _weaponSO = _value as WeaponSO;
        UpdateVisuals();
        SendOnInit();
    }

    private void UpdateVisuals()
    {
        _spriteRenderer.sprite = _weaponSO.SpriteIcon;
    }
}
