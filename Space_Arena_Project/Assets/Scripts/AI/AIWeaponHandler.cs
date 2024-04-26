using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponHandler : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _currentWeapon = null;
    [SerializeField, ReadOnly] WeaponRotator _weaponRotator = null;
    [SerializeField, ReadOnly] bool _isShooting = false;

    public void PullTrigger()
    {
        if (_isShooting) return;

        StartCoroutine(PullTrigger_routine());
    }

    public void ReleaseTrigger()
    {
        _currentWeapon.ReleaseTrigger();
    }

    public void RotateCurrentWeapon()
    {
        _weaponRotator.LookAt(default);
    }

    private IEnumerator PullTrigger_routine()
    {
        _isShooting = true;
        _currentWeapon.PullTrigger();

        yield return new WaitForSeconds(0.2f);

        _isShooting = false;
        _currentWeapon.ReleaseTrigger();
    }
}
