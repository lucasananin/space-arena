using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstWeapon : WeaponBehaviour
{
    [Title("// Burst")]
    [SerializeField] float _burstRate = 0.5f;
    [SerializeField] int _maxShootCount = 3;

    [Title("// Debug - Burst")]
    [SerializeField, ReadOnly] float _nextBurst = 0;
    [SerializeField, ReadOnly] int _currentShootCount = 0;
    [SerializeField, ReadOnly] bool _isBursting = false;

    protected override void Awake()
    {
        base.Awake();
        _nextBurst = _burstRate;
    }

    private void FixedUpdate()
    {
        if (_isBursting)
        {
            _nextFire += _nextFire < _weaponSO.FireRate ? Time.fixedDeltaTime : 0;

            if (_nextFire >= _weaponSO.FireRate)
            {
                Shoot();

                _currentShootCount++;

                if (_currentShootCount >= _maxShootCount)
                {
                    _nextFire = _weaponSO.FireRate;
                    _isBursting = false;
                }
            }
        }
        else
        {
            _nextBurst += _nextBurst < _burstRate ? Time.fixedDeltaTime : 0;
        }
    }

    public override void PullTrigger()
    {
        if (_nextBurst < _burstRate) return;
        if (_isOverheated) return;

        _nextBurst -= _burstRate;
        _currentShootCount = 0;
        _isBursting = true;
        base.PullTrigger();
    }

    public override void ReleaseTrigger()
    {
        base.ReleaseTrigger();
    }
}
