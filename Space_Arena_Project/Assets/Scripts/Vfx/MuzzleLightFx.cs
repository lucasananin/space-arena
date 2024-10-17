using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleLightFx : LightFadeFx
{
    [Title("// Weapon")]
    [SerializeField] WeaponBehaviour _weapon = null;

    private void OnEnable()
    {
        _weapon.OnShoot += Init;
    }

    private void OnDisable()
    {
        _weapon.OnShoot -= Init;
    }
}
