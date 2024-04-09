using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoWeapon : WeaponBehaviour
{
    //[SerializeField] float _fireRate = 0.1f;
    //[SerializeField, ReadOnly] float _nextFire = 0;
    [Space]
    [SerializeField] float _maxChargeTime = 1f;
    [SerializeField] bool _isAutoCharge = false;
    [SerializeField] bool _hasChargeWeakShot = false;
    [SerializeField, ReadOnly] bool _isCharging = false;
    [SerializeField, ReadOnly] bool _hasShot = false;
    [SerializeField, ReadOnly] float _chargeTimer = 0f;

    //private void Awake()
    //{
    //    _nextFire = _fireRate;
    //}

    private void FixedUpdate()
    {
        _nextFire += _nextFire < _fireRate ? Time.fixedDeltaTime : 0;

        // hasCharge.
        if (_maxChargeTime <= 0) return;

        if (_isCharging)
        {
            _chargeTimer += _chargeTimer < _maxChargeTime ? Time.fixedDeltaTime : 0;
        }
        else
        {
            _chargeTimer -= _chargeTimer > 0 ? Time.fixedDeltaTime : 0;
        }

        if (_isAutoCharge)
        {
            if (_chargeTimer >= _maxChargeTime)
            {
                _chargeTimer -= _maxChargeTime;
                _isCharging = false;
                //_nextFire -= _fireRate;
                //Shoot();
                _hasShot = true;
                ShootChargedShot();
                //Debug.Log($"// AutoChargeShot!");
            }
            //else
            //{
            //    // Shoot(); tiro comum.
            //    Debug.Log($"// DefaultShot!");
            //}
        }
    }

    public override void PullTrigger()
    {
        _hasShot = false;

        if (_nextFire < _fireRate) return;

        //_nextFire -= _fireRate;
        _isCharging = true;
        //_hasShot = false;

        if (_maxChargeTime <= 0)
        {
            _isCharging = false;
            //_nextFire -= _fireRate;
            _hasShot = true;
            Shoot();
        }
    }

    public override void ReleaseTrigger()
    {
        if (_maxChargeTime <= 0) return;
        if (_hasShot) return;
        //_isCharging = false;
        //if (!_isCharging)
        //{
        //    return;
        //}

        if (!_isAutoCharge)
        {
            if (_chargeTimer >= _maxChargeTime)
            {
                _chargeTimer -= _maxChargeTime;
                //_isCharging = false;
                //_nextFire -= _fireRate;
                //Shoot();
                _hasShot = true;
                ShootChargedShot();
            }
            else if (_hasChargeWeakShot && _chargeTimer < _maxChargeTime)
            {
                _hasShot = true;
                Shoot();
                Debug.Log($"// ChargeWeakShot!");
            }
        }
        else if (_hasChargeWeakShot && _chargeTimer < _maxChargeTime)
        {
            _hasShot = true;
            Shoot();
            Debug.Log($"// ChargeWeakShot!");
        }

        _isCharging = false;
    }
}
