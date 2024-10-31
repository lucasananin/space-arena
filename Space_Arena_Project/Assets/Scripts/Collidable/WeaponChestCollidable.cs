using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponChestCollidable : CollidableBehaviour
{
    [SerializeField] LootDropper _lootDropper = null;
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] Sprite _openedSprite = null;
    [SerializeField] Transform _spawnPoint = null;
    [Space]
    [SerializeField] UnityEvent OnOpen = null;

    public override void Collide(CollectableAgent _agent)
    {
        if (_collided) return;

        _collided = true;
        _renderer.sprite = _openedSprite;
        _lootDropper.Drop(_spawnPoint.position);
        OnOpen?.Invoke();
    }
}
