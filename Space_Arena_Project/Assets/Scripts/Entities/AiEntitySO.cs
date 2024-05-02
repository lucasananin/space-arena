using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity_", menuName = "SO/Entities/AI Entity")]
public class AiEntitySO : EntitySO
{
    [Title("// Shoot Target")]
    [SerializeField, Range(0f, 99f)] float _shootDistance = 0f;

    [Title("// Move Close To Target")]
    [SerializeField, Range(0.5f, 9f)] float _moveClose_radius = 0f;
    [SerializeField] bool _stopSearchingPathOnClose = true;
    [SerializeField, Range(0, 99)] int _maxNumberOfTries = 10;
    [SerializeField] Vector2 _minMaxTimeUntilSearchPath = default;

    public float ShootDistance { get => _shootDistance; private set => _shootDistance = value; }

    public float MoveClose_radius { get => _moveClose_radius; private set => _moveClose_radius = value; }
    public bool StopSearchingPathOnClose { get => _stopSearchingPathOnClose; private set => _stopSearchingPathOnClose = value; }
    public int MaxNumberOfTries { get => _maxNumberOfTries; private set => _maxNumberOfTries = value; }
    public Vector2 MinMaxTimeUntilSearchPath { get => _minMaxTimeUntilSearchPath; private set => _minMaxTimeUntilSearchPath = value; }
}
