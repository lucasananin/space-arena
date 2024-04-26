using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponHandler : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _currentWeapon = null;
    [SerializeField] WeaponRotator _weaponRotator = null;
    [SerializeField, ReadOnly] bool _isShooting = false;

    public event System.Action onStoppedShooting = null;

    public void PullTrigger()
    {
        if (_isShooting) return;

        StartCoroutine(PullTrigger_routine());
    }

    public void ReleaseTrigger()
    {
        _currentWeapon.ReleaseTrigger();
    }

    public void RotateWeapon(Vector3 _position)
    {
        _weaponRotator.LookAt(_position);
    }

    public void ResetWeaponRotation()
    {
        _weaponRotator.ResetRotation();
    }

    public bool IsShooting()
    {
        return _isShooting;
    }

    private IEnumerator PullTrigger_routine()
    {
        _isShooting = true;
        _currentWeapon.PullTrigger();

        yield return new WaitForSeconds(0.1f);

        _isShooting = false;
        _currentWeapon.ReleaseTrigger();
        onStoppedShooting?.Invoke();
    }
}