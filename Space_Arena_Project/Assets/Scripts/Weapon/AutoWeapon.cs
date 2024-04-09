using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon : WeaponBehaviour
{
    [SerializeField, ReadOnly] bool _isHoldingTrigger = false;

    private void FixedUpdate()
    {
        _nextFire += _nextFire < _fireRate ? Time.fixedDeltaTime : 0;

        if (_isHoldingTrigger && _nextFire >= _fireRate)
        {
            Shoot();
        }
    }

    public override void PullTrigger()
    {
        _isHoldingTrigger = true;
        base.PullTrigger();
    }

    public override void ReleaseTrigger()
    {
        _isHoldingTrigger = false;
        base.ReleaseTrigger();
    }
}
