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

    [Title("// Wait Until Search Path")]
    [SerializeField] Vector2 _minMaxTimeUntilSearchPath = default;

    [Title("// Move Close To Target")]
    [SerializeField] Vector2 _minMax_moveCloseRadius = Vector2.one;
    [SerializeField] bool _stopSearchingPathOnClose = true;
    [SerializeField, Range(0, 99)] int _maxNumberOfTries = 10;

    public AiEntity EntityPrefab { get => _entityPrefab; private set => _entityPrefab = value; }
    //public bool AlwaysFaceTarget { get => _alwaysFaceTarget; set => _alwaysFaceTarget = value; }
    public Vector2 MinMaxTimeUntilSearchPath { get => _minMaxTimeUntilSearchPath; private set => _minMaxTimeUntilSearchPath = value; }
    public Vector2 MinMax_moveCloseRadius { get => _minMax_moveCloseRadius; private set => _minMax_moveCloseRadius = value; }
    public bool StopSearchingPathOnClose { get => _stopSearchingPathOnClose; private set => _stopSearchingPathOnClose = value; }
    public int MaxNumberOfTries { get => _maxNumberOfTries; private set => _maxNumberOfTries = value; }
}
