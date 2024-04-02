using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoWeapon : AbstractWeaponBehaviour
{
    [SerializeField] float _fireRate = 0.1f;
    [SerializeField, ReadOnly] float _nextFire = 0;

    public AbstractProjectileBehaviour _projectile = null;
    public Transform _muzzle = null;

    private void Awake()
    {
        _nextFire = _fireRate;
    }

    private void FixedUpdate()
    {
        _nextFire += _nextFire < _fireRate ? Time.fixedDeltaTime : 0;
    }

    public override void PullTrigger()
    {
        if (_nextFire < _fireRate) return;

        _nextFire -= _fireRate;
        InvokeOnShootEvent();
        Instantiate(_projectile, _muzzle.position, transform.rotation);
    }

    public override void ReleaseTrigger()
    {
        //
    }
}
