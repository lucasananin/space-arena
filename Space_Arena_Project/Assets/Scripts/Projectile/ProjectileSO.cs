using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile_", menuName = "SO/Combat/Projectile Data")]
public class ProjectileSO : ScriptableObject
{
    [Title("// General")]
    [SerializeField] ProjectileBehaviour _prefab = null;
    [SerializeField] AmmoSO _ammoSO = null;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField] LayerMask _entityLayerMask = default;
    [SerializeField] LayerMask _obstacleLayerMask = default;
    [SerializeField] Vector2 _spawnPositionOffset = default;

    public ProjectileBehaviour Prefab { get => _prefab; private set => _prefab = value; }
    public AmmoSO AmmoSO { get => _ammoSO; private set => _ammoSO = value; }
    public LayerMask LayerMask { get => _layerMask; private set => _layerMask = value; }
    public LayerMask EntityLayerMask { get => _entityLayerMask; private set => _entityLayerMask = value; }
    public LayerMask ObstacleLayerMask { get => _obstacleLayerMask; private set => _obstacleLayerMask = value; }
    public Vector2 SpawnPositionOffset { get => _spawnPositionOffset; private set => _spawnPositionOffset = value; }
}

[System.Serializable]
public class ProjectileStats
{
    [Title("// General")]
    [SerializeField] Vector2 _destroyTimeRange = default;
    [SerializeField, Range(1, 99)] int _maxPierceCount = 1;
    [SerializeField, Range(0f, 99)] float _explosionRadius = 0f;
    [SerializeField] bool _canDamageProjectiles = false;
    [Title("// Auto Aim")]
    [SerializeField] bool _canAutoAim = false;
    [SerializeField] float _autoAimDistance = 0f;
    [SerializeField] float _autoAimAngle = 0f;
    [Title("- PROJECTILE SPECIFICS -", null, TitleAlignments.Centered)]
    [Title("// Physical Projectile: Properties")]
    //[SerializeField, Range(0f, 99f)] float _moveSpeed = 0f;
    [SerializeField] Vector2 _moveSpeedRange = default;
    [SerializeField] bool _destroyOnCollision = false;
    [SerializeField] bool _destroyOnStop = false;
    [SerializeField] bool _useAccelerationCurve = false;
    [SerializeField] bool _invertAcceleration = false;
    [SerializeField] AnimationCurve _accelerationCurve = null;
    [SerializeField, Range(0f, 10f)] float _acelerationMultiplier = 0;
    [Title("// Cast Projectile: Properties")]
    [SerializeField, Range(0f, 99f)] float _maxCastDistance = 0f;
    [Title("// Guided Projectile: Properties")]
    [SerializeField, Range(0f, 9f)] float _maxGuidedRadius = 0f;

    public Vector2 DestroyTimeRange { get => _destroyTimeRange; private set => _destroyTimeRange = value; }
    public int MaxPierceCount { get => _maxPierceCount; private set => _maxPierceCount = value; }
    public float ExplosionRadius { get => _explosionRadius; private set => _explosionRadius = value; }
    public bool CanDamageProjectiles { get => _canDamageProjectiles; private set => _canDamageProjectiles = value; }
    public bool CanAutoAim { get => _canAutoAim; private set => _canAutoAim = value; }
    public float AutoAimDistance { get => _autoAimDistance; private set => _autoAimDistance = value; }
    public float AutoAimAngle { get => _autoAimAngle; private set => _autoAimAngle = value; }
    //public float MoveSpeed { get => _moveSpeed; private set => _moveSpeed = value; }
    public Vector2 MoveSpeedRange { get => _moveSpeedRange; set => _moveSpeedRange = value; }
    public bool DestroyOnCollision { get => _destroyOnCollision; private set => _destroyOnCollision = value; }
    public bool DestroyOnStop { get => _destroyOnStop; private set => _destroyOnStop = value; }
    public bool UseAccelerationCurve { get => _useAccelerationCurve; private set => _useAccelerationCurve = value; }
    public bool InvertAcceleration { get => _invertAcceleration; private set => _invertAcceleration = value; }
    public AnimationCurve AccelerationCurve { get => _accelerationCurve; private set => _accelerationCurve = value; }
    public float AcelerationMultiplier { get => _acelerationMultiplier; private set => _acelerationMultiplier = value; }
    public float MaxCastDistance { get => _maxCastDistance; private set => _maxCastDistance = value; }
    public float MaxGuidedRadius { get => _maxGuidedRadius; private set => _maxGuidedRadius = value; }

    //[Button]
    //public void Fodase()
    //{
    //    _moveSpeedRange = new Vector2(_moveSpeed, _moveSpeed);
    //}
}