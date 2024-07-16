using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "SO/Combat/Weapon Data")]
public class WeaponSO : ScriptableObject
{
    [Title("// General")]
    [SerializeField, PreviewField] Sprite _iconSprite = null;
    [SerializeField] WeaponBehaviour _weaponPrefab = null;
    [SerializeField] string _id = null;
    [SerializeField] string _displayName = null;

    [Title("// Holster")]
    [SerializeField] Vector3 _holsterPosition = default;
    [SerializeField] Vector3 _holsterEuler = default;

    [Title("// Projectiles")]
    [SerializeField] ProjectileSO _projectileSO = null;
    [SerializeField] ProjectileSO _chargedProjectileSO = null;

    [Title("// Stats")]
    [SerializeField] WeaponStats _stats = null;
    [SerializeField] ProjectileStats _projectileStats = null;

    [Title("- WEAPON PROPERTIES -", null, TitleAlignments.Centered)]
    [Title("// Damage")]
    [SerializeField, Range(0, 99)] int _damage = 1;
    [SerializeField, Range(0, 99)] int _chargeShotDamage = 1;
    [SerializeField, Range(0, 99)] int _explosiveDamage = 1;
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
    [SerializeField, Range(0, 360)] float _shootAngle = 0;
    [SerializeReference] ShootArcMode _arcMode = default;
    [Title("// Ammo")]
    [SerializeField, Range(1, 36)] int _projectilesPerShot = 1;
    [SerializeField, Range(0, 99)] int _ammoPerShot = 1;
    [Title("- WEAPON SPECIFICS -", null, TitleAlignments.Centered)]
    [Title("// Semi-Auto Behaviour: Properties")]
    [SerializeField] bool _isAutoChargeType = false;
    [SerializeField] bool _hasChargeWeakShot = false;
    [Title("// Burst Behaviour: Properties")]
    [SerializeField, Range(0f, 9f)] float _burstRate = 0f;
    [SerializeField, Range(0, 99)] int _maxBurstShootCount = 0;

    [Title("- PROJECTILE PROPERTIES -", null, TitleAlignments.Centered)]
    [Title("// General")]
    [SerializeField] Vector2 _destroyTimeRange = default;
    [SerializeField, Range(1, 99)] int _maxPierceCount = 1;
    [SerializeField, Range(0f, 99)] float _explosionRadius = 0f;
    [SerializeField] bool _canDamageProjectiles = false;
    [Title("// Auto Aim")]
    [SerializeField] bool _canAutoAim = false;
    [SerializeField] float _autoAimDistance = 0f;
    [SerializeField] float _autoAimAngle = 0f;
    [Title("- PROJECTILE SPECIFICS -", null, TitleAlignments.Centered)]
    [Title("// Physical Projectile: Properties")]
    [SerializeField, Range(0f, 99f)] float _moveSpeed = 0f;
    [SerializeField] bool _destroyOnCollision = false;
    [SerializeField] bool _destroyOnStop = false;
    [SerializeField] bool _useAccelerationCurve = false;
    [SerializeField] bool _invertAcceleration = false;
    [SerializeField] AnimationCurve _accelerationCurve = null;
    [SerializeField, Range(0f, 10f)] float _acelerationMultiplier = 0;
    [Title("// Cast Projectile: Properties")]
    [SerializeField, Range(0f, 99f)] float _maxCastDistance = 0f;
    [Title("// Guided Projectile: Properties")]
    [SerializeField, Range(0f, 9f)] float _maxGuidedRadius = 0f;

    public Sprite SpriteIcon { get => _iconSprite; private set => _iconSprite = value; }
    public WeaponBehaviour WeaponPrefab { get => _weaponPrefab; private set => _weaponPrefab = value; }
    public string Id { get => _id; private set => _id = value; }
    public string DisplayName { get => _displayName; private set => _displayName = value; }
    public Vector3 HolsterPosition { get => _holsterPosition; private set => _holsterPosition = value; }
    public Vector3 HolsterEuler { get => _holsterEuler; private set => _holsterEuler = value; }
    public ProjectileSO ProjectileSO { get => _projectileSO; private set => _projectileSO = value; }
    public ProjectileSO ChargedProjectileSO { get => _chargedProjectileSO; private set => _chargedProjectileSO = value; }

    public int Damage { get => _damage; private set => _damage = value; }
    public int ChargeShotDamage { get => _chargeShotDamage; private set => _chargeShotDamage = value; }
    public int ExplosiveDamage { get => _explosiveDamage; private set => _explosiveDamage = value; }
    public float FireRate { get => _fireRate; private set => _fireRate = value; }
    public float MaxChargeTime { get => _maxChargeTime; private set => _maxChargeTime = value; }
    public float MaxHeat { get => _maxHeat; private set => _maxHeat = value; }
    public float HeatPerShot { get => _heatPerShot; private set => _heatPerShot = value; }
    public float HeatDecreasePerSecond { get => _heatDecreasePerSecond; private set => _heatDecreasePerSecond = value; }
    public float MaxOverheatTime { get => _maxOverheatTime; private set => _maxOverheatTime = value; }
    public int ProjectilesPerShot { get => _projectilesPerShot; private set => _projectilesPerShot = value; }
    public int AmmoPerShot { get => _ammoPerShot; set => _ammoPerShot = value; }
    public bool IsAutoChargeType { get => _isAutoChargeType; private set => _isAutoChargeType = value; }
    public bool HasChargeWeakShot { get => _hasChargeWeakShot; private set => _hasChargeWeakShot = value; }
    public float BurstRate { get => _burstRate; private set => _burstRate = value; }
    public int MaxBurstShootCount { get => _maxBurstShootCount; private set => _maxBurstShootCount = value; }

    public Vector2 DestroyTimeRange { get => _destroyTimeRange; private set => _destroyTimeRange = value; }
    public int MaxPierceCount { get => _maxPierceCount; private set => _maxPierceCount = value; }
    public float ExplosionRadius { get => _explosionRadius; private set => _explosionRadius = value; }
    public bool CanDamageProjectiles { get => _canDamageProjectiles; private set => _canDamageProjectiles = value; }
    public bool CanAutoAim { get => _canAutoAim; private set => _canAutoAim = value; }
    public float AutoAimDistance { get => _autoAimDistance; private set => _autoAimDistance = value; }
    public float AutoAimAngle { get => _autoAimAngle; private set => _autoAimAngle = value; }
    public float MoveSpeed { get => _moveSpeed; private set => _moveSpeed = value; }
    public bool DestroyOnCollision { get => _destroyOnCollision; private set => _destroyOnCollision = value; }
    public bool DestroyOnStop { get => _destroyOnStop; private set => _destroyOnStop = value; }
    public bool UseAccelerationCurve { get => _useAccelerationCurve; private set => _useAccelerationCurve = value; }
    public bool InvertAcceleration { get => _invertAcceleration; private set => _invertAcceleration = value; }
    public AnimationCurve AccelerationCurve { get => _accelerationCurve; private set => _accelerationCurve = value; }
    public float AcelerationMultiplier { get => _acelerationMultiplier; private set => _acelerationMultiplier = value; }
    public float MaxCastDistance { get => _maxCastDistance; private set => _maxCastDistance = value; }
    public float MaxGuidedRadius { get => _maxGuidedRadius; private set => _maxGuidedRadius = value; }

    public float ShootAngle { get => _shootAngle; set => _shootAngle = value; }
    public ShootArcMode ArcMode { get => _arcMode; set => _arcMode = value; }
    public WeaponStats Stats { get => _stats; private set => _stats = value; }

    //private void OnValidate()
    //{
    //    _stats.Copy(this);
    //}

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

    public Quaternion GetProjectileRotation(int _projectileIndex)
    {
        if (_shootAngle <= 0)
        {
            return Quaternion.identity;
        }

        float _halfAngle = _shootAngle / 2f;

        if (_arcMode == ShootArcMode.Spread && _projectilesPerShot > 1)
        {
            float _sector = _shootAngle;
            float _divisionCount = _sector / (_projectilesPerShot - 1);
            float _angle = -_halfAngle + _divisionCount * _projectileIndex;
            return Quaternion.AngleAxis(_angle, Vector3.forward);
        }
        else
        {
            float _randomAngle = Random.Range(-_halfAngle, _halfAngle);
            return Quaternion.AngleAxis(_randomAngle, Vector3.forward);
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    [Title("// Damage")]
    [SerializeField, Range(0, 99)] int _damage = 1;
    [SerializeField, Range(0, 99)] int _chargeShotDamage = 1;
    [SerializeField, Range(0, 99)] int _explosiveDamage = 1;
    [Title("// Fire Rate")]
    [SerializeField, Range(0.01f, 9f)] float _fireRate = 0.1f;
    [Title("// Charge")]
    [SerializeField, Range(0f, 9f)] float _chargeTime = 0f;
    [Title("// Heat")]
    [SerializeField, Range(0f, 99f)] float _maxHeat = 0f;
    [SerializeField, Range(0f, 99f)] float _heatPerShot = 0f;
    [SerializeField, Range(0f, 99f)] float _heatDecreasePerSecond = 0f;
    [SerializeField, Range(0f, 99f)] float _maxOverheatTime = 0f;
    [Title("// Precision")]
    [SerializeField, Range(0, 360)] float _shootAngle = 0;
    [SerializeReference] ShootArcMode _arcMode = default;
    [Title("// Ammo")]
    [SerializeField, Range(1, 36)] int _projectilesPerShot = 1;
    [SerializeField, Range(0, 99)] int _ammoPerShot = 1;
    [Title("- WEAPON SPECIFICS -", null, TitleAlignments.Centered)]
    [Title("// Semi-Auto Behaviour: Properties")]
    [SerializeField] bool _isAutoChargeType = false;
    [SerializeField] bool _hasChargeWeakShot = false;
    [Title("// Burst Behaviour: Properties")]
    [SerializeField, Range(0f, 9f)] float _burstRate = 0f;
    [SerializeField, Range(0, 99)] int _shotsPerBurst = 0;

    public int Damage { get => _damage; private set => _damage = value; }
    public int ChargeShotDamage { get => _chargeShotDamage; private set => _chargeShotDamage = value; }
    public int ExplosiveDamage { get => _explosiveDamage; private set => _explosiveDamage = value; }
    public float FireRate { get => _fireRate; private set => _fireRate = value; }
    public float ChargeTime { get => _chargeTime; set => _chargeTime = value; }
    public float MaxHeat { get => _maxHeat; private set => _maxHeat = value; }
    public float HeatPerShot { get => _heatPerShot; private set => _heatPerShot = value; }
    public float HeatDecreasePerSecond { get => _heatDecreasePerSecond; private set => _heatDecreasePerSecond = value; }
    public float MaxOverheatTime { get => _maxOverheatTime; private set => _maxOverheatTime = value; }
    public float ShootAngle { get => _shootAngle; set => _shootAngle = value; }
    public ShootArcMode ArcMode { get => _arcMode; set => _arcMode = value; }
    public int ProjectilesPerShot { get => _projectilesPerShot; private set => _projectilesPerShot = value; }
    public int AmmoPerShot { get => _ammoPerShot; set => _ammoPerShot = value; }
    public bool IsAutoChargeType { get => _isAutoChargeType; private set => _isAutoChargeType = value; }
    public bool HasChargeWeakShot { get => _hasChargeWeakShot; private set => _hasChargeWeakShot = value; }
    public float BurstRate { get => _burstRate; private set => _burstRate = value; }
    public int ShotsPerBurst { get => _shotsPerBurst; set => _shotsPerBurst = value; }

    public void Copy(WeaponSO _so)
    {
        _damage = _so.Damage;
        _chargeShotDamage = _so.ChargeShotDamage;
        _explosiveDamage = _so.ExplosiveDamage;
        _fireRate = _so.FireRate;
        _chargeTime = _so.MaxChargeTime;
        _maxHeat = _so.MaxHeat;
        _heatPerShot = _so.HeatPerShot;
        _heatDecreasePerSecond = _so.HeatDecreasePerSecond;
        _maxOverheatTime = _so.MaxOverheatTime;
        _shootAngle = _so.ShootAngle;
        _arcMode = _so.ArcMode;
        _projectilesPerShot = _so.ProjectilesPerShot;
        _ammoPerShot = _so.AmmoPerShot;
        _isAutoChargeType = _so.IsAutoChargeType;
        _hasChargeWeakShot = _so.HasChargeWeakShot;
        _burstRate = _so.BurstRate;
        _shotsPerBurst = _so.MaxBurstShootCount;
    }
}

public enum ShootArcMode
{
    Random,
    Spread,
}