using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile_", menuName = "SO/Combat/Projectile Data")]
public class ProjectileSO : ScriptableObject
{
    [Title("// General")]
    [SerializeField] ProjectileBehaviour _prefab = null;
    [SerializeField] TagCollectionSO _tagCollectionSO = null;
    [SerializeField] AmmoSO _ammoSO = null;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField] Vector2 _spawnPositionOffset = default;
    [SerializeField] Vector2 _minMaxTimeUntilDestroy = default;
    [SerializeField, Range(1, 99)] int _maxPierceCount = 1;
    [SerializeField, Range(0f, 99)] float _explosionRadius = 0f;

    [Title("// Auto Aim")]
    [SerializeField] bool _canAutoRotate = false;
    [SerializeField] LayerMask _entityLayerMask = default;
    [SerializeField] float _maxAngle = 0f;

    [Title("// Physical")]
    [SerializeField, Range(0f, 99f)] float _moveSpeed = 0f;
    [SerializeField] bool _destroyOnCollision = true;
    [Space]
    [SerializeField] bool _useAccelerationCurve = false;
    [SerializeField] AnimationCurve _accelerationCurve = null;
    [SerializeField] bool _invertAcceleration = false;
    [SerializeField, Range(0f, 10f)] float _acelerationMultiplier = 1f;
    [SerializeField] bool _destroyOnStop = false;

    [Title("// Cast")]
    [SerializeField, Range(0f, 99f)] float _maxCastDistance = 0f;

    [Title("// Guided ")]
    [SerializeField, Range(0f, 9f)] float _maxPositionRadius = 0f;

    public ProjectileBehaviour Prefab { get => _prefab; private set => _prefab = value; }
    public string[] Tags { get => _tagCollectionSO.Tags; }
    public AmmoSO AmmoSO { get => _ammoSO; private set => _ammoSO = value; }

    public bool CanAutoRotate { get => _canAutoRotate; private set => _canAutoRotate = value; }
    public LayerMask EntityLayerMask { get => _entityLayerMask; private set => _entityLayerMask = value; }
    public float MaxAngle { get => _maxAngle; private set => _maxAngle = value; }

    public LayerMask LayerMask { get => _layerMask; private set => _layerMask = value; }
    public Vector2 SpawnPositionOffset { get => _spawnPositionOffset; private set => _spawnPositionOffset = value; }
    public Vector2 MinMaxTimeUntilDestroy { get => _minMaxTimeUntilDestroy; private set => _minMaxTimeUntilDestroy = value; }
    public int MaxPierceCount { get => _maxPierceCount; private set => _maxPierceCount = value; }
    public float ExplosionRadius { get => _explosionRadius; private set => _explosionRadius = value; }

    public float MoveSpeed { get => _moveSpeed; private set => _moveSpeed = value; }
    public bool DestroyOnCollision { get => _destroyOnCollision; private set => _destroyOnCollision = value; }

    public bool UseAccelerationCurve { get => _useAccelerationCurve; set => _useAccelerationCurve = value; }
    public bool InvertAcceleration { get => _invertAcceleration; set => _invertAcceleration = value; }
    public AnimationCurve AccelerationCurve { get => _accelerationCurve; set => _accelerationCurve = value; }
    public float AcelerationMultiplier { get => _acelerationMultiplier; set => _acelerationMultiplier = value; }
    public bool DestroyOnStop { get => _destroyOnStop; private set => _destroyOnStop = value; }

    public float MaxCastDistance { get => _maxCastDistance; private set => _maxCastDistance = value; }

    public float MaxPositionRadius { get => _maxPositionRadius; private set => _maxPositionRadius = value; }
}
