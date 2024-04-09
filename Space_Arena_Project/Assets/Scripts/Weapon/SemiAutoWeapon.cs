using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoWeapon : WeaponBehaviour
{
    [Space]
    [SerializeField] float _maxChargeTime = 1f;
    [SerializeField] bool _isAutoChargeType = false;
    [SerializeField] bool _hasChargeWeakShot = false;
    [SerializeField, ReadOnly] bool _isCharging = false;
    [SerializeField, ReadOnly] bool _hasShot = false;
    [SerializeField, ReadOnly] float _chargeTimer = 0f;

    private void FixedUpdate()
    {
        _nextFire += _nextFire < _fireRate ? Time.fixedDeltaTime : 0;

        if (_maxChargeTime <= 0) return;

        if (_isCharging)
        {
            _chargeTimer += _chargeTimer < _maxChargeTime ? Time.fixedDeltaTime : 0;
        }
        else
        {
            _chargeTimer -= _chargeTimer > 0 ? Time.fixedDeltaTime : 0;
        }

        if (_isAutoChargeType)
        {
            if (_chargeTimer >= _maxChargeTime)
            {
                _chargeTimer -= _maxChargeTime;
                _isCharging = false;
                _hasShot = true;
                ShootChargedShot();
            }
        }
    }

    public override void PullTrigger()
    {
        _hasShot = false;

        if (_nextFire < _fireRate) return;

        _isCharging = true;

        if (_maxChargeTime <= 0)
        {
            _isCharging = false;
            _hasShot = true;
            Shoot();
        }

        base.PullTrigger();
    }

    public override void ReleaseTrigger()
    {
        if (_maxChargeTime <= 0)
        {
            base.ReleaseTrigger();
            return;
        }

        if (_hasShot) return;

        if (!_isAutoChargeType)
        {
            if (_chargeTimer >= _maxChargeTime)
            {
                _chargeTimer -= _maxChargeTime;
                _hasShot = true;
                ShootChargedShot();
            }
            else if (_hasChargeWeakShot && _chargeTimer < _maxChargeTime)
            {
                _hasShot = true;
                Shoot();
            }
        }
        else if (_hasChargeWeakShot && _chargeTimer < _maxChargeTime)
        {
            _hasShot = true;
            Shoot();
        }

        _isCharging = false;
        base.ReleaseTrigger();
    }
}
