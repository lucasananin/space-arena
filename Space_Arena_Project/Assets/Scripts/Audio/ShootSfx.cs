using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSfx : AudioCue
{
    [Title("// Weapons")]
    [SerializeField] WeaponBehaviour _weapon = null;

    private void OnValidate()
    {
        _weapon = GetComponent<WeaponBehaviour>();
    }

    private void OnEnable()
    {
        _weapon.OnShoot += PlayAudioCue;
    }

    private void OnDisable()
    {
        _weapon.OnShoot -= PlayAudioCue;
    }
}
