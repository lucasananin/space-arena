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

    [Title("// Move Close To Target")]
    [SerializeField] Vector2 _moveCloseRange = Vector2.one;
    [SerializeField] bool _stopMovingOnClose = true;
    [SerializeField, Range(0, 99)] int _maxNumberOfTries = 10;

    public AiEntity EntityPrefab { get => _entityPrefab; private set => _entityPrefab = value; }
    //public bool AlwaysFaceTarget { get => _alwaysFaceTarget; set => _alwaysFaceTarget = value; }
    public Vector2 MoveRateRange { get => _moveRateRange; private set => _moveRateRange = value; }

    public Vector2 MinMax_moveCloseRadius { get => _moveCloseRange; private set => _moveCloseRange = value; }
    public bool StopmovingOnClose { get => _stopMovingOnClose; private set => _stopMovingOnClose = value; }
    public int MaxNumberOfTries { get => _maxNumberOfTries; private set => _maxNumberOfTries = value; }
}
