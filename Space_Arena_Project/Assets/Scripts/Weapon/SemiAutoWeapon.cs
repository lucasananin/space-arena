using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoWeapon : WeaponBehaviour
{
    [Title("// Semi Auto")]
    [SerializeField] protected bool _isAutoChargeType = false;
    [SerializeField] protected bool _hasChargeWeakShot = false;

    [Title("// Debug - Semi Auto")]
    [SerializeField, ReadOnly] protected bool _hasShotCharge = false;

    private void FixedUpdate()
    {
        _nextFire += _nextFire < _fireRate ? Time.fixedDeltaTime : 0;

        SetChargeTimer();

        if (_isAutoChargeType && HasEnoughChargeTimer())
        {
            _isCharging = false;
            _hasShotCharge = true;
            ShootChargedShot();
        }
    }

    public override void PullTrigger()
    {
        _hasShotCharge = false;

        if (_nextFire < _fireRate) return;

        if (HasChargeTime())
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
        if (HasChargeTime())
        {
            TryManualChargeShot();
        }

        base.ReleaseTrigger();
    }

    private void TryManualChargeShot()
    {
        if (_hasShotCharge) return;

        if (!_isAutoChargeType)
        {
            if (HasEnoughChargeTimer())
            {
                _hasShotCharge = true;
                ShootChargedShot();
            }
            else if (CanWeakShot())
            {
                _hasShotCharge = true;
                Shoot();
            }
        }
        else if (CanWeakShot())
        {
            _hasShotCharge = true;
            Shoot();
        }

        _isCharging = false;
    }

    private bool CanWeakShot()
    {
        return _hasChargeWeakShot && _chargeTimer < _maxChargeTime;
    }
}
