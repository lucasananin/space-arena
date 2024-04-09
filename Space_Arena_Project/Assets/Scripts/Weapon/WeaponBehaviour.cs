using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] protected WeaponSO _weaponSO = null;
    [SerializeField] protected GameObject _characterSource = null;
    [SerializeField] protected Transform _muzzle = null;
    [Space]
    [SerializeField] protected float _fireRate = 0.1f;
    [SerializeField, ReadOnly] protected float _nextFire = 0;

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
        ProjectileBehaviour _prefab = _weaponSO.GetProjectilePrefab();
        ProjectileBehaviour _projectile = Instantiate(_prefab, _muzzle.position, transform.rotation);
        ShootModel _shootModel = new ShootModel(_characterSource, this);
        _projectile.Init(_shootModel);

        onShoot?.Invoke();
        //Debug.Log($"// Shoot!");
    }

    public virtual void ShootChargedShot()
    {
        _nextFire -= _fireRate;
        ProjectileBehaviour _prefab = _weaponSO.GetChargedProjectilePrefab();
        ProjectileBehaviour _projectile = Instantiate(_prefab, _muzzle.position, transform.rotation);
        ShootModel _shootModel = new ShootModel(_characterSource, this);
        _projectile.Init(_shootModel);

        onShoot?.Invoke();
    }

    //public abstract void PullTrigger();
    //public abstract void ReleaseTrigger();

    public virtual void PullTrigger()
    {
        onPullTrigger?.Invoke();
    }

    public virtual void ReleaseTrigger()
    {
        onReleaseTrigger?.Invoke();
    }
}
