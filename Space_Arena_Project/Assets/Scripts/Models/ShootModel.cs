using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootModel
{
    [SerializeField] EntityBehaviour _entitySource = null;
    [SerializeField] WeaponBehaviour _weaponSource = null;
    [SerializeField] ProjectileSO _projectileSO = null;

    public EntityBehaviour EntitySource { get => _entitySource; private set => _entitySource = value; }
    public WeaponBehaviour WeaponSource { get => _weaponSource; private set => _weaponSource = value; }
    public ProjectileSO ProjectileSO { get => _projectileSO; private set => _projectileSO = value; }

    public ShootModel(EntityBehaviour _entitySource, WeaponBehaviour _weaponSource, ProjectileSO _projectileSO)
    {
        this._entitySource = _entitySource;
        this._weaponSource = _weaponSource;
        this._projectileSO = _projectileSO;
    }
}
