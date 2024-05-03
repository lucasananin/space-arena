using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootModel
{
    [SerializeField] EntityBehaviour _entitySource = null;
    [SerializeField] WeaponBehaviour _weaponSource = null;

    public EntityBehaviour EntitySource { get => _entitySource; private set => _entitySource = value; }
    public WeaponBehaviour WeaponSource { get => _weaponSource; private set => _weaponSource = value; }

    public ShootModel(EntityBehaviour _entitySource, WeaponBehaviour _weaponSource)
    {
        this._entitySource = _entitySource;
        this._weaponSource = _weaponSource;
    }
}
