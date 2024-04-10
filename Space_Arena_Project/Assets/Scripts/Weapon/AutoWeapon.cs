using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon : WeaponBehaviour
{
    [Title("// Debug - Automatic")]
    [SerializeField, ReadOnly] bool _isHoldingTrigger = false;

    private void FixedUpdate()
    {
        _nextFire += _nextFire < _fireRate ? Time.fixedDeltaTime : 0;

        SetChargeTimer();

        if (_chargeTimer >= _maxChargeTime)
        {
            if (_isHoldingTrigger && _nextFire >= _fireRate)
            {
                Shoot();
            }
        }
    }

    public override void PullTrigger()
    {
        _isCharging = true;
        _isHoldingTrigger = true;
        base.PullTrigger();
    }

    public override void ReleaseTrigger()
    {
        _isCharging = false;
        _isHoldingTrigger = false;
        base.ReleaseTrigger();
    }
}
