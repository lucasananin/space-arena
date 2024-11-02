using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingShotSFX : AudioCue
{
    [Title("// Weapon")]
    [SerializeField] WeaponBehaviour _weapon = null;

    private void OnEnable()
    {
        _weapon.OnPullTrigger += PlayAudioCue;
        _weapon.OnReleaseTrigger += StopAudioCue;
        _weapon.OnShoot += StopAudioCue;
    }

    private void OnDisable()
    {
        _weapon.OnPullTrigger -= PlayAudioCue;
        _weapon.OnReleaseTrigger -= StopAudioCue;
        _weapon.OnShoot -= StopAudioCue;
    }
}
