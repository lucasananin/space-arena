using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoWeapon : WeaponBehaviour
{
    [Title("// Debug - Semi Auto")]
    [SerializeField, ReadOnly] protected bool _hasShotCharge = false;

    private void FixedUpdate()
    {
        _nextFire += _nextFire < _weaponSO.Stats.FireRate ? Time.fixedDeltaTime : 0;

        UpdateChargeTimer();

        if (CanAutoChargeShot())
        {
            _isCharging = false;
            _hasShotCharge = true;
            ShootChargedShot();
        }
    }

    public override void PullTrigger()
    {
        _hasShotCharge = false;

        if (!HasAmmo()) return;
        if (_isOverheated) return;
        if (_nextFire < _weaponSO.Stats.FireRate) return;

        if (_weaponSO.HasChargeTime())
        {
            _isCharging = true;
        }
        else
        {
            _isCharging = false;
            _hasShotCharge = true;
            Shoot();
        }

        base.PullTrigger();
    }

    public override void ReleaseTrigger()
    {
        if (_weaponSO.HasChargeTime())
        {
            TryManualChargeShot();
        }

        base.ReleaseTrigger();
    }

    private void TryManualChargeShot()
    {
        if (_hasShotCharge) return;
        if (_isOverheated) return;

        if (!_weaponSO.Stats.IsAutoChargeType)
        {
            if (HasEnoughChargeTimer())
            {
                _hasShotCharge = true;
                ShootChargedShot();
            }
            //else if (CanWeakShot())
            //{
            //    _hasShotCharge = true;
            //    Shoot();
            //}
        }
        //else if (CanWeakShot())
        //{
        //    _hasShotCharge = true;
        //    Shoot();
        //}

        _isCharging = false;
    }

    //private bool CanWeakShot()
    //{
    //    return _weaponSO.Stats.HasChargeWeakShot && _chargeTimer < _weaponSO.Stats.ChargeTime && HasAmmo();
    //}

    private bool CanAutoChargeShot()
    {
        return _weaponSO.Stats.IsAutoChargeType && HasEnoughChargeTimer() && !_isOverheated && HasAmmo();
    }
}
