using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "SO/Combat/Weapon Data")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] ProjectileSO _projectileSO = null;
    [SerializeField] ProjectileSO _chargedProjectileSO = null;

    public ProjectileSO ProjectileSO { get => _projectileSO; private set => _projectileSO = value; }
    public ProjectileSO ChargedProjectileSO { get => _chargedProjectileSO; private set => _chargedProjectileSO = value; }
}
