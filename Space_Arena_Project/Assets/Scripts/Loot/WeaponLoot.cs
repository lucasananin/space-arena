using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoot : LootBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer = null;
    [SerializeField, ReadOnly] WeaponSO _weaponSO = null;

    public override void Init(ScriptableObject _value)
    {
        _weaponSO = _value as WeaponSO;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        _spriteRenderer.sprite = _weaponSO.SpriteIcon;
    }
}
