using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponHandler : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _currentWeapon = null;
    [SerializeField, ReadOnly] WeaponRotator _weaponRotator = null;
    [SerializeField, ReadOnly] HealthBehaviour _targetHealth = null;

    //private void Awake()
    //{
    //    _weaponRotator = _currentWeapon.GetComponent<WeaponRotator>();
    //}

    public void PullTrigger()
    {
        _currentWeapon.PullTrigger();
    }

    public void ReleaseTrigger()
    {
        _currentWeapon.ReleaseTrigger();
    }

    public void RotateCurrentWeapon()
    {
        _weaponRotator.LookAt(default);
    }

    public void SetTargetEntity(GameObject _gameobject)
    {
        _targetHealth = _gameobject.GetComponent<HealthBehaviour>();
    }

    public bool HasTargetEntity()
    {
        return _targetHealth != null && _targetHealth.IsAlive();
    }
}
