using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChestCollidable : CollidableBehaviour
{
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] Sprite _openedSprite = null;
    [SerializeField] WeaponLoot _weaponLootPrefab = null;
    [SerializeField] Transform _spawnTransform = null;
    [SerializeField] WeaponSO _weaponSo = null;

    public override void Collide(CollectableAgent _agent)
    {
        if (_collided) return;

        _collided = true;
        _renderer.sprite = _openedSprite;

        var _instance = Instantiate(_weaponLootPrefab, _spawnTransform.position, Quaternion.identity, transform);
        _instance.Init(_weaponSo);
    }
}
