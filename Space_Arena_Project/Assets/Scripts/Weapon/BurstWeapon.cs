using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstWeapon : AbstractWeapon
{
    [SerializeField] float _burstRate = 0.5f;
    [SerializeField, ReadOnly] float _nextBurst = 0;
    [SerializeField] int _maxShootCount = 3;
    [SerializeField, ReadOnly] int _currentShootCount = 0;
    [SerializeField] float _fireRate = 0.1f;
    [SerializeField, ReadOnly] float _nextFire = 0f;
    [SerializeField, ReadOnly] bool _isBursting = false;

    private void Awake()
    {
        _nextBurst = _burstRate;
        _nextFire = _fireRate;
    }

    private void FixedUpdate()
    {
        if (_nextBurst < _burstRate)
        {
            _nextBurst += Time.fixedDeltaTime;
        }

        if (_nextFire < _fireRate)
        {
            _nextFire += Time.fixedDeltaTime;
        }

        if (!_isBursting) return;

        if (_nextBurst >= _burstRate)
        {
            _nextBurst -= _burstRate;
        }

        if (_nextFire >= _fireRate)
        {
            _nextFire -= _nextFire;
            _currentShootCount++;
            InvokeOnShootEvent();

            if (_currentShootCount >= _maxShootCount)
            {
                _nextFire = _fireRate;
                _isBursting = false;
            }
        }
    }

    public override void PullTrigger()
    {
        if (_isBursting) return;
        if (_nextBurst < _burstRate) return;

        _currentShootCount = 0;
        _isBursting = true;
    }

    public override void ReleaseTrigger()
    {
        //
    }
}
