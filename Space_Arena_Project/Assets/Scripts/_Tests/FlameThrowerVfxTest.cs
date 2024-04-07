using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerVfxTest : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weaponBehaviour = null;
    [SerializeField] ParticleSystem _particleSystem = null;

    private void OnEnable()
    {
        _weaponBehaviour.onPullTrigger += _weaponBehaviour_onPullTrigger;
        _weaponBehaviour.onReleaseTrigger += _weaponBehaviour_onReleaseTrigger;
    }

    private void _weaponBehaviour_onPullTrigger()
    {
        _particleSystem.Play();
    }

    private void _weaponBehaviour_onReleaseTrigger()
    {
        _particleSystem.Stop();
    }
}
