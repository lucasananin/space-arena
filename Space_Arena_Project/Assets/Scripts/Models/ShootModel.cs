using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootModel
{
    [SerializeField] GameObject _characterSource = null;
    [SerializeField] WeaponBehaviour _weaponSource = null;

    public GameObject CharacterSource { get => _characterSource; private set => _characterSource = value; }
    public WeaponBehaviour WeaponSource { get => _weaponSource; private set => _weaponSource = value; }

    public ShootModel(GameObject _characterSource, WeaponBehaviour _weaponSource)
    {
        this._characterSource = _characterSource;
        this._weaponSource = _weaponSource;
    }
}
