using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity_", menuName = "SO/Entities/AI Entity")]
public class AiEntitySO : EntitySO
{
    [SerializeField] AiEntity _entityPrefab = null;

    //[Title("// Flip")]
    //[SerializeField] bool _alwaysFaceTarget = false;

    [Title("// Time Until Move Again")]
    [SerializeField] Vector2 _moveRateRange = default;
    //[SerializeField] bool _repathOnTargetFarAway = true;

    [Title("// Move Close To Target")]
    [SerializeField] Vector2 _moveCloseRange = Vector2.one;
    [SerializeField] bool _stopMovingOnClose = true;
    [SerializeField, Range(0, 99)] int _maxNumberOfTries = 10;

    [Title("// Flank - Properties")]
    [SerializeField] Vector2 _flankRange = Vector2.one;
    [SerializeField] float _flankDistance = 3f;

    [Title("// Cower in Fear - Properties")]
    [SerializeField] Vector2 _cowerTimeRange = Vector2.zero;

    public AiEntity EntityPrefab { get => _entityPrefab; private set => _entityPrefab = value; }
    //public bool AlwaysFaceTarget { get => _alwaysFaceTarget; set => _alwaysFaceTarget = value; }
    public Vector2 MoveRateRange { get => _moveRateRange; private set => _moveRateRange = value; }
    //public bool RepathOnTargetFarAway { get => _repathOnTargetFarAway; private set => _repathOnTargetFarAway = value; }

    public Vector2 MoveCloseRange { get => _moveCloseRange; private set => _moveCloseRange = value; }
    public bool StopMovingOnClose { get => _stopMovingOnClose; private set => _stopMovingOnClose = value; }
    public int MaxNumberOfTries { get => _maxNumberOfTries; private set => _maxNumberOfTries = value; }

    public Vector2 FlankRange { get => _flankRange; private set => _flankRange = value; }
    public float FlankDistance { get => _flankDistance; private set => _flankDistance = value; }

    public Vector2 CowerTimeRange { get => _cowerTimeRange; private set => _cowerTimeRange = value; }
}
