using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "SO/Combat/Weapon Data")]
public class WeaponSO : ScriptableObject
{
    [Title("// Projectiles")]
    [SerializeField] ProjectileSO _projectileSO = null;
    [SerializeField] ProjectileSO _chargedProjectileSO = null;

    [Title("// Fire Rate")]
    [SerializeField, Range(0.01f, 9f)] float _fireRate = 0.1f;

    [Title("// Charge")]
    [SerializeField, Range(0f, 9f)] float _maxChargeTime = 0f;

    [Title("// Heat")]
    [SerializeField, Range(0f, 99f)] float _maxHeat = 0f;
    [SerializeField, Range(0f, 99f)] float _heatPerShot = 0f;
    [SerializeField, Range(0f, 99f)] float _heatDecreasePerSecond = 0f;
    [SerializeField, Range(0f, 99f)] float _maxOverheatTime = 0f;

    [Title("// Precision")]
    [SerializeField, Range(0, 180)] int _maxShootAngle = 0;

    [Title("// Projectiles per shot")]
    [SerializeField, Range(1, 36)] int _projectilesPerShot = 1;

    public ProjectileSO ProjectileSO { get => _projectileSO; private set => _projectileSO = value; }
    public ProjectileSO ChargedProjectileSO { get => _chargedProjectileSO; private set => _chargedProjectileSO = value; }
    public float FireRate { get => _fireRate; private set => _fireRate = value; }
    public float MaxChargeTime { get => _maxChargeTime; private set => _maxChargeTime = value; }
    public float MaxHeat { get => _maxHeat; private set => _maxHeat = value; }
    public float HeatPerShot { get => _heatPerShot; private set => _heatPerShot = value; }
    public float HeatDecreasePerSecond { get => _heatDecreasePerSecond; private set => _heatDecreasePerSecond = value; }
    public float MaxOverheatTime { get => _maxOverheatTime; private set => _maxOverheatTime = value; }
    public int MaxShootAngle { get => _maxShootAngle; private set => _maxShootAngle = value; }
    public int ProjectilesPerShot { get => _projectilesPerShot; private set => _projectilesPerShot = value; }

    public bool HasChargeTime()
    {
        return _maxChargeTime > 0;
    }

    public bool CanOverheat()
    {
        return _maxHeat > 0;
    }
}
