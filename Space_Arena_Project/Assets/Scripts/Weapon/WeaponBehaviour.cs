using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    [Title("// Weapon")]
    [SerializeField] protected WeaponSO _weaponSO = null;
    [SerializeField] protected Transform _muzzle = null;

    [Title("// Debug - Weapon")]
    [SerializeField, ReadOnly] protected EntityBehaviour _entitySource = null;
    [SerializeField, ReadOnly] protected AmmoHandler _ammoHandler = null;
    [SerializeField, ReadOnly] protected float _nextFire = 0;
    [SerializeField, ReadOnly] protected float _chargeTimer = 0f;
    [SerializeField, ReadOnly] protected bool _isCharging = false;
    [SerializeField, ReadOnly] protected float _currentHeat = 0f;
    [SerializeField, ReadOnly] protected float _overheatTimer = 0f;
    [SerializeField, ReadOnly] protected bool _isOverheated = false;

    public event System.Action<WeaponBehaviour> onInit = null;
    public event System.Action onShoot = null;
    public event System.Action onPullTrigger = null;
    public event System.Action onReleaseTrigger = null;

    const float HEAT_OFFSET = 0.9f;

    public WeaponSO WeaponSO { get => _weaponSO; private set => _weaponSO = value; }
    public Transform Muzzle { get => _muzzle; private set => _muzzle = value; }

    protected virtual void Awake()
    {
        _nextFire = _weaponSO.FireRate;
    }

    protected virtual void Update()
    {
        DecreaseHeat();
    }

    public virtual void Init(EntityBehaviour _entityBehaviour, AmmoHandler _ammoHandler)
    {
        this._ammoHandler = _ammoHandler;
        Init(_entityBehaviour);
    }

    public virtual void Init(EntityBehaviour _entityBehaviour)
    {
        _entitySource = _entityBehaviour;
        onInit?.Invoke(this);
    }

    public virtual void PullTrigger()
    {
        onPullTrigger?.Invoke();
    }

    public virtual void ReleaseTrigger()
    {
        onReleaseTrigger?.Invoke();
    }

    public virtual void Shoot()
    {
        _nextFire -= _weaponSO.FireRate;
        PrepareProjectile(_weaponSO.ProjectileSO);
        IncreaseHeat(_weaponSO.HeatPerShot);
        DecreaseAmmo();
        onShoot?.Invoke();
    }

    public virtual void ShootChargedShot()
    {
        _nextFire -= _weaponSO.FireRate;
        _chargeTimer -= _weaponSO.MaxChargeTime;
        PrepareProjectile(_weaponSO.ChargedProjectileSO);
        IncreaseHeat(_weaponSO.MaxHeat + HEAT_OFFSET);
        DecreaseAmmo();
        onShoot?.Invoke();
    }

    private void PrepareProjectile(ProjectileSO _projectileSO)
    {
        for (int i = 0; i < _weaponSO.ProjectilesPerShot; i++)
        {
            var _position = CalculateProjectilePosition(_projectileSO);
            var _rotation = CalculateProjectileRotation(i);
            var _projectile = Instantiate(_projectileSO.Prefab, _position, _rotation);
            var _shootModel = new ShootModel(_entitySource, this, _projectileSO);
            _projectile.Init(_shootModel);
        }
    }

    protected Quaternion CalculateProjectileRotation(int _projectileIndex)
    {
        if (_weaponSO.MaxShootAngle <= 0)
        {
            return transform.rotation;
        }
        else
        {
            Quaternion _rotationOffset;

            if (_weaponSO.ProjectilesPerShot > 1)
            {
                float _sector = _weaponSO.MaxShootAngle * 2f;
                float _divisionCount = _sector / (_weaponSO.ProjectilesPerShot - 1);
                float _angle = -_weaponSO.MaxShootAngle + _divisionCount * _projectileIndex;
                _rotationOffset = Quaternion.AngleAxis(_angle, Vector3.forward);
            }
            else
            {
                float _randomAngle = Random.Range(-_weaponSO.MaxShootAngle, _weaponSO.MaxShootAngle);
                _rotationOffset = Quaternion.AngleAxis(_randomAngle, Vector3.forward);
            }

            return transform.rotation * _rotationOffset;
        }
    }

    protected Vector3 CalculateProjectilePosition(ProjectileSO _projectileSO)
    {
        if (_projectileSO.SpawnPositionOffset == Vector2.zero)
        {
            return _muzzle.position;
        }
        else
        {
            Vector3 _xOffset = transform.right * _projectileSO.SpawnPositionOffset.x;
            Vector3 _yOffset = transform.up * _projectileSO.SpawnPositionOffset.y;
            return _muzzle.position + _xOffset + _yOffset;
        }
    }

    protected void UpdateChargeTimer()
    {
        if (!_weaponSO.HasChargeTime()) return;

        if (_isCharging)
        {
            _chargeTimer += _chargeTimer < _weaponSO.MaxChargeTime ? Time.fixedDeltaTime : 0;
        }
        else
        {
            _chargeTimer -= _chargeTimer > 0 ? Time.fixedDeltaTime : 0;
        }
    }

    protected bool HasEnoughChargeTimer()
    {
        return _chargeTimer >= _weaponSO.MaxChargeTime;
    }

    protected void IncreaseHeat(float _value)
    {
        if (!_weaponSO.CanOverheat()) return;

        _currentHeat += _value;

        if (_currentHeat >= _weaponSO.MaxHeat)
        {
            _currentHeat = _weaponSO.MaxHeat;
            _overheatTimer = 0;
            _isOverheated = true;
        }
    }

    protected void DecreaseHeat()
    {
        if (!_weaponSO.CanOverheat()) return;

        _currentHeat -= _currentHeat > 0 ? _weaponSO.HeatDecreasePerSecond * Time.deltaTime : 0;

        if (_isOverheated)
        {
            _overheatTimer += _overheatTimer < _weaponSO.MaxOverheatTime ? Time.deltaTime : 0;

            if (_overheatTimer >= _weaponSO.MaxOverheatTime)
            {
                _isOverheated = false;
            }
        }
    }

    public float GetPullTriggerTotalTime()
    {
        return _weaponSO.GetPullTriggerTotalTime();
    }

    public float GetTimeUntilAnotherShot()
    {
        return _overheatTimer + _weaponSO.GetTimeUntilAnotherShot();
    }

    public string GetId()
    {
        return _weaponSO.Id;
    }

    private void DecreaseAmmo()
    {
        _ammoHandler?.DecreaseAmmo(_weaponSO);
    }

    public bool HasAmmo()
    {
        if (_ammoHandler is null)
        {
            return true;
        }
        else
        {
            return _ammoHandler.HasAmmo(_weaponSO);
        }
    }

    public int GetDamage()
    {
        return _weaponSO.Damage;
    }

    public int GetExplosiveDamage()
    {
        return _weaponSO.ExplosiveDamage;
    }
}
