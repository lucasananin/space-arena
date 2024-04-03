using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "SO/Weapons/Weapon Data")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] ProjectileSO _projectileSO = null;

    public ProjectileBehaviour GetProjectilePrefab()
    {
        return _projectileSO.Prefab;
    }
}
