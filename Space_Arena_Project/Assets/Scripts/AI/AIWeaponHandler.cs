using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponHandler : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _currentWeapon = null;
    [SerializeField, ReadOnly] WeaponRotator _weaponRotator = null;

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
}
