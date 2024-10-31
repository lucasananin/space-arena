using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandlerSFX : MonoBehaviour
{
    [SerializeField] PlayerWeaponHandler _weaponHandler = null;
    [SerializeField] AudioCue _weaponAddedSfx = null;
    [SerializeField] AudioCue _weaponSwappedSfx = null;

    private void OnEnable()
    {
        _weaponHandler.OnWeaponAdded += _weaponAddedSfx.PlayAudioCue;
        _weaponHandler.OnWeaponSwapped += _weaponSwappedSfx.PlayAudioCue;
    }

    private void OnDisable()
    {
        _weaponHandler.OnWeaponAdded -= _weaponAddedSfx.PlayAudioCue;
        _weaponHandler.OnWeaponSwapped -= _weaponSwappedSfx.PlayAudioCue;
    }
}
