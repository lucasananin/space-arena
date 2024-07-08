using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity_", menuName = "SO/Entities/AI Entity")]
public class AiEntitySO : EntitySO
{
    [Title("// General")]
    [SerializeField] AiEntity _entityPrefab = null;
    [SerializeField] bool _stopMovingOnClose = true;
    [SerializeField] bool _stopMovingOnTargetAcquired = false;
    [SerializeField] bool _repathOnTargetFarAway = true;
    [SerializeField, Range(0f, 9f)] float _minDistance = 0f;
    [SerializeField, Range(0, 99)] int _maxNumberOfTries = 10;

    //[Title("// Flip")]
    //[SerializeField] bool _alwaysFaceTarget = false;

    [Title("// Time Until Move Again")]
    [SerializeField] Vector2 _moveRateRange = default;

    [Title("// Move Close To Target - Properties")]
    [SerializeField] Vector2 _moveCloseRange = Vector2.one;

    [Title("// Flank Target - Properties")]
    [SerializeField] Vector2 _flankRange = Vector2.one;
    [SerializeField] float _flankDistance = 3f;

    [Title("// Cower in Fear - Properties")]
    [SerializeField] Vector2 _cowerTimeRange = Vector2.zero;

    [Title("// Charging Movement - Properties")]
    [SerializeField] float _chargingSpeedMultiplier = 0f;
    [SerializeField] float _chargingDistance = 0f;
    [SerializeField] Vector2 _chargingWaitRange = Vector2.one;

    [Title("// Explode Itself - Properties")]
    [SerializeField] float _timeUntilExplode = 0f;

    public AiEntity EntityPrefab { get => _entityPrefab; private set => _entityPrefab = value; }
    public bool StopMovingOnClose { get => _stopMovingOnClose; private set => _stopMovingOnClose = value; }
    public bool StopMovingOnTargetAcquired { get => _stopMovingOnTargetAcquired; private set => _stopMovingOnTargetAcquired = value; }
    public bool RepathOnTargetFarAway { get => _repathOnTargetFarAway; private set => _repathOnTargetFarAway = value; }
    public float MinDistance { get => _minDistance; set => _minDistance = value; }
    public int MaxNumberOfTries { get => _maxNumberOfTries; private set => _maxNumberOfTries = value; }

    //public bool AlwaysFaceTarget { get => _alwaysFaceTarget; set => _alwaysFaceTarget = value; }
    public Vector2 MoveRateRange { get => _moveRateRange; private set => _moveRateRange = value; }

    public Vector2 MoveCloseRange { get => _moveCloseRange; private set => _moveCloseRange = value; }

    public Vector2 FlankRange { get => _flankRange; private set => _flankRange = value; }
    public float FlankDistance { get => _flankDistance; private set => _flankDistance = value; }

    public Vector2 CowerTimeRange { get => _cowerTimeRange; private set => _cowerTimeRange = value; }

    public float ChargingSpeedMultiplier { get => _chargingSpeedMultiplier; private set => _chargingSpeedMultiplier = value; }
    public float ChargingDistance { get => _chargingDistance; private set => _chargingDistance = value; }
    public Vector2 ChargingWaitRange { get => _chargingWaitRange; private set => _chargingWaitRange = value; }
    public float TimeUntilExplode { get => _timeUntilExplode; private set => _timeUntilExplode = value; }
}
