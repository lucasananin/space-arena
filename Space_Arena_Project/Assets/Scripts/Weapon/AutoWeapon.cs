using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon : AbstractWeapon
{
    [SerializeField] float _fireRate = 0.1f;
    [SerializeField, ReadOnly] float _nextFire = 0;
    [SerializeField, ReadOnly] bool _isHoldingTrigger = false;

    private void FixedUpdate()
    {
        if (_nextFire < _fireRate)
        {
            _nextFire += Time.fixedDeltaTime;
        }

        if (!_isHoldingTrigger) return;

        if (_nextFire >= _fireRate)
        {
            _nextFire -= _fireRate;
            InvokeOnShootEvent();
        }
    }

    public override void PullTrigger()
    {
        _isHoldingTrigger = true;
    }

    public override void ReleaseTrigger()
    {
        _isHoldingTrigger = false;
    }
}
