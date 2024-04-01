using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoWeapon : AbstractWeapon
{
    [SerializeField] float _fireRate = 0.1f;
    [SerializeField, ReadOnly] float _nextFire = 0;

    private void FixedUpdate()
    {
        if (_nextFire < _fireRate)
        {
            _nextFire += Time.fixedDeltaTime;
        }
    }

    public override void PullTrigger()
    {
        if (_nextFire < _fireRate) return;

        _nextFire -= _fireRate;
        InvokeOnShootEvent();
    }

    public override void ReleaseTrigger()
    {
        //
    }
}
