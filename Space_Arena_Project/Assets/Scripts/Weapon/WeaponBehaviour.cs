using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    [Title("// Weapon")]
    [SerializeField] protected WeaponSO _weaponSO = null;
    [SerializeField] protected GameObject _characterSource = null;
    [SerializeField] protected Transform _muzzle = null;
    [SerializeField] protected float _fireRate = 0.1f;
    [SerializeField] protected float _maxChargeTime = 0f;
    [SerializeField] protected float _maxHeat = 0f;
    [SerializeField] protected float _heatPerShot = 1f;
    [SerializeField] protected float _heatDecreasePerSecond = 1f;
    [SerializeField] protected float _maxOverheatTime = 2f;
    [SerializeField, Range(0, 180)] protected int _maxShootAngle = 0;
    [SerializeField, Range(1, 36)] protected int _projectilesPerShot = 1;

    [Title("// Debug - Weapon")]
    [SerializeField, ReadOnly] protected float _nextFire = 0;
    [SerializeField, ReadOnly] protected float _chargeTimer = 0f;
    [SerializeField, ReadOnly] protected bool _isCharging = false;
    [SerializeField, ReadOnly] protected float _currentHeat = 0f;
    [SerializeField, ReadOnly] protected float _overheatTimer = 0f;
    [SerializeField, ReadOnly] protected bool _isOverheated = false;

    public event System.Action onShoot = null;
    public event System.Action onPullTrigger = null;
    public event System.Action onReleaseTrigger = null;

    protected virtual void Awake()
    {
        _nextFire = _fireRate;
    }

    protected virtual void Update()
    {
        DecreaseHeat();
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
        _nextFire -= _fireRate;
        PrepareProjectile(_weaponSO.GetProjectilePrefab());
        IncreaseHeat(_heatPerShot);
        onShoot?.Invoke();
    }

    public virtual void ShootChargedShot()
    {
        _nextFire -= _fireRate;
        _chargeTimer -= _maxChargeTime;
        PrepareProjectile(_weaponSO.GetChargedProjectilePrefab());
        float _heatOffset = 0.9f;
        IncreaseHeat(_maxHeat + _heatOffset);
        onShoot?.Invoke();
    }

    private void PrepareProjectile(ProjectileBehaviour _prefab)
    {
        for (int i = 0; i < _projectilesPerShot; i++)
        {
            Quaternion _rotation = CalculateProjectileRotation(i);
            ProjectileBehaviour _projectile = Instantiate(_prefab, _muzzle.position, _rotation);
            ShootModel _shootModel = new ShootModel(_characterSource, this);
            _projectile.Init(_shootModel);
        }
    }

    protected Quaternion CalculateProjectileRotation(int _projectileIndex)
    {
        if (_maxShootAngle <= 0)
        {
            return transform.rotation;
        }
        else
        {
            Quaternion _rotation = transform.rotation;
            Quaternion _rotationOffset = Quaternion.identity;

            if (_projectilesPerShot > 1)
            {
                float _sector = _maxShootAngle * 2f;
                float _divisionCount = _sector / (_projectilesPerShot - 1);
                float _angle = -_maxShootAngle + _divisionCount * _projectileIndex;
                _rotationOffset = Quaternion.AngleAxis(_angle, Vector3.forward);
            }
            else
            {
                float _randomAngle = Random.Range(-_maxShootAngle, _maxShootAngle);
                _rotationOffset = Quaternion.AngleAxis(_randomAngle, Vector3.forward);
            }

            return _rotation * _rotationOffset;
        }
    }

    protected void SetChargeTimer()
    {
        if (!HasChargeTime()) return;

        if (_isCharging)
        {
            _chargeTimer += _chargeTimer < _maxChargeTime ? Time.fixedDeltaTime : 0;
        }
        else
        {
            _chargeTimer -= _chargeTimer > 0 ? Time.fixedDeltaTime : 0;
        }
    }

    protected bool HasChargeTime()
    {
        return _maxChargeTime > 0;
    }

    protected bool HasEnoughChargeTimer()
    {
        return _chargeTimer >= _maxChargeTime;
    }

    protected void IncreaseHeat(float _value)
    {
        if (!CanOverheat()) return;

        _currentHeat += _value;

        if (_currentHeat >= _maxHeat)
        {
            _currentHeat = _maxHeat;
            _overheatTimer = 0;
            _isOverheated = true;
        }
    }

    protected void DecreaseHeat()
    {
        if (!CanOverheat()) return;

        _currentHeat -= _currentHeat > 0 ? _heatDecreasePerSecond * Time.deltaTime : 0;

        if (_isOverheated)
        {
            _overheatTimer += _overheatTimer < _maxOverheatTime ? Time.deltaTime : 0;

            if (_overheatTimer >= _maxOverheatTime)
            {
                _isOverheated = false;
            }
        }
    }

    protected bool CanOverheat()
    {
        return _maxHeat > 0;
    }
}
