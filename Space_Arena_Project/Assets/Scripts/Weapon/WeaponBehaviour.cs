using Sirenix.OdinInspector;
using System;
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
    
    [Title("// Debug - Weapon")]
    [SerializeField, ReadOnly] protected float _nextFire = 0;
    [SerializeField, ReadOnly] protected float _chargeTimer = 0f;
    [SerializeField, ReadOnly] protected bool _isCharging = false;

    public event Action onShoot = null;
    public event Action onPullTrigger = null;
    public event Action onReleaseTrigger = null;

    protected virtual void Awake()
    {
        _nextFire = _fireRate;
    }

    public virtual void Shoot()
    {
        _nextFire -= _fireRate;
        PrepareProjectile(_weaponSO.GetProjectilePrefab());
        onShoot?.Invoke();
    }

    public virtual void ShootChargedShot()
    {
        _nextFire -= _fireRate;
        _chargeTimer -= _maxChargeTime;
        PrepareProjectile(_weaponSO.GetChargedProjectilePrefab());
        onShoot?.Invoke();
    }

    private void PrepareProjectile(ProjectileBehaviour _prefab)
    {
        // Precisão.
        // Quantidade de projéteis.
        ProjectileBehaviour _projectile = Instantiate(_prefab, _muzzle.position, transform.rotation);
        ShootModel _shootModel = new ShootModel(_characterSource, this);
        _projectile.Init(_shootModel);
    }

    public virtual void PullTrigger()
    {
        onPullTrigger?.Invoke();
    }

    public virtual void ReleaseTrigger()
    {
        onReleaseTrigger?.Invoke();
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
}
