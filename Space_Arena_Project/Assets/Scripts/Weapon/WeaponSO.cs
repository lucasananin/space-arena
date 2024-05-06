using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "SO/Combat/Weapon Data")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] protected string _id = null;

    [Title("// General")]
    [SerializeField, PreviewField] Sprite _iconSprite = null;
    [SerializeField] WeaponBehaviour _weaponPrefab = null;
    [SerializeField] string _displayName = null;

    [Title("// Projectiles")]
    [SerializeField] ProjectileSO _projectileSO = null;
    [SerializeField] ProjectileSO _chargedProjectileSO = null;

    [Title("// Fire Rate")]
    [SerializeField, Range(0.01f, 9f)] float _fireRate = 0.1f;

    [Title("// Charge")]
    [SerializeField, Range(0f, 9f)] float _maxChargeTime = 0f;

    [Title("// Heat")]
    [SerializeField, Range(0f, 99f)] float _maxHeat = 0f;
    [SerializeField, Range(0f, 99f)] float _heatPerShot = 0f;
    [SerializeField, Range(0f, 99f)] float _heatDecreasePerSecond = 0f;
    [SerializeField, Range(0f, 99f)] float _maxOverheatTime = 0f;

    [Title("// Precision")]
    [SerializeField, Range(0, 180)] int _maxShootAngle = 0;

    [Title("// Projectiles per shot")]
    [SerializeField, Range(1, 36)] int _projectilesPerShot = 1;

    [Title("// Semi Auto - Properties")]
    [SerializeField] bool _isAutoChargeType = false;
    [SerializeField] bool _hasChargeWeakShot = false;

    [Title("// Burst - Properties")]
    [SerializeField, Range(0f, 9f)] float _burstRate = 0f;
    [SerializeField, Range(0, 99)] int _maxBurstShootCount = 0;

    public string Id { get => _id; private set => _id = value; }

    public Sprite SpriteIcon { get => _iconSprite; private set => _iconSprite = value; }
    public WeaponBehaviour WeaponPrefab { get => _weaponPrefab; private set => _weaponPrefab = value; }
    public string DisplayName { get => _displayName; private set => _displayName = value; }

    public ProjectileSO ProjectileSO { get => _projectileSO; private set => _projectileSO = value; }
    public ProjectileSO ChargedProjectileSO { get => _chargedProjectileSO; private set => _chargedProjectileSO = value; }

    public float FireRate { get => _fireRate; private set => _fireRate = value; }
    public float MaxChargeTime { get => _maxChargeTime; private set => _maxChargeTime = value; }
    public float MaxHeat { get => _maxHeat; private set => _maxHeat = value; }
    public float HeatPerShot { get => _heatPerShot; private set => _heatPerShot = value; }
    public float HeatDecreasePerSecond { get => _heatDecreasePerSecond; private set => _heatDecreasePerSecond = value; }
    public float MaxOverheatTime { get => _maxOverheatTime; private set => _maxOverheatTime = value; }
    public int MaxShootAngle { get => _maxShootAngle; private set => _maxShootAngle = value; }
    public int ProjectilesPerShot { get => _projectilesPerShot; private set => _projectilesPerShot = value; }

    public bool IsAutoChargeType { get => _isAutoChargeType; private set => _isAutoChargeType = value; }
    public bool HasChargeWeakShot { get => _hasChargeWeakShot; private set => _hasChargeWeakShot = value; }

    public float BurstRate { get => _burstRate; private set => _burstRate = value; }
    public int MaxBurstShootCount { get => _maxBurstShootCount; private set => _maxBurstShootCount = value; }

    public bool HasChargeTime()
    {
        return _maxChargeTime > 0;
    }

    public bool CanOverheat()
    {
        return _maxHeat > 0;
    }

    public float GetPullTriggerTotalTime()
    {
        float _offset = 0.1f;
        return _maxChargeTime + _offset;
    }

    public float GetTimeUntilAnotherShot()
    {
        float _totalFireRate = _maxBurstShootCount > 0 ? _fireRate * _maxBurstShootCount : _fireRate;
        float _offset = 0.1f;
        return _totalFireRate + _burstRate + _offset;
    }
}
