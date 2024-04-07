using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon : WeaponBehaviour
{
    [SerializeField] float _fireRate = 0.1f;
    [SerializeField, ReadOnly] float _nextFire = 0;
    [SerializeField, ReadOnly] bool _isHoldingTrigger = false;

    private void Awake()
    {
        _nextFire = _fireRate;
    }

    private void FixedUpdate()
    {
        _nextFire += _nextFire < _fireRate ? Time.fixedDeltaTime : 0;

        if (_nextFire >= _fireRate && _isHoldingTrigger)
        {
            _nextFire -= _fireRate;
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
