using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "SO/Combat/Weapon Data")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] ProjectileSO _projectileSO = null;
    [SerializeField] ProjectileSO _chargedProjectileSO = null;

    public ProjectileBehaviour GetProjectilePrefab()
    {
        return _projectileSO.Prefab;
    }

    public ProjectileBehaviour GetChargedProjectilePrefab()
    {
        return _chargedProjectileSO.Prefab;
    }
}
