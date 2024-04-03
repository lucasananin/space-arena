using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] protected WeaponSO _weaponSO = null;
    [SerializeField] protected GameObject _characterSource = null;
    [SerializeField] protected Transform _muzzle = null;

    public event Action onShoot = null;

    public virtual void Shoot()
    {
        ProjectileBehaviour _projectile = Instantiate(_weaponSO.GetProjectilePrefab(), _muzzle.position, transform.rotation);
        ShootModel _shootModel = new ShootModel(_characterSource, this);
        _projectile.Init(_shootModel);

        onShoot?.Invoke();
        Debug.Log($"// Shoot!");
    }

    public abstract void PullTrigger();
    public abstract void ReleaseTrigger();
}
