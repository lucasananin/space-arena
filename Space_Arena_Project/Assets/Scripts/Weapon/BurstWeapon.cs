using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstWeapon : WeaponBehaviour
{
    [Title("// Debug - Burst")]
    [SerializeField, ReadOnly] float _nextBurst = 0;
    [SerializeField, ReadOnly] int _currentShootCount = 0;
    [SerializeField, ReadOnly] bool _isBursting = false;

    protected override void Awake()
    {
        base.Awake();
        _nextBurst = _weaponSO.BurstRate;
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

                if (_currentShootCount >= _weaponSO.MaxBurstShootCount)
                {
                    _nextFire = _weaponSO.FireRate;
                    _isBursting = false;
                }
            }
        }
        else
        {
            _nextBurst += _nextBurst < _weaponSO.BurstRate? Time.fixedDeltaTime : 0;
        }
    }

    public override void PullTrigger()
    {
        if (!HasAmmo()) return;
        if (_isOverheated) return;
        if (_nextBurst < _weaponSO.BurstRate) return;

        _nextBurst -= _weaponSO.BurstRate;
        _currentShootCount = 0;
        _isBursting = true;
        base.PullTrigger();
    }

    public override void ReleaseTrigger()
    {
        base.ReleaseTrigger();
    }
}
