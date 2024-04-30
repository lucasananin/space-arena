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
    [SerializeField] float _moveClose_radius = 0f;
    [SerializeField] bool _stopOnCloseEnough = true;

    public float ShootDistance { get => _shootDistance; private set => _shootDistance = value; }

    public float MoveClose_radius { get => _moveClose_radius; private set => _moveClose_radius = value; }
    public bool StopOnCloseEnough { get => _stopOnCloseEnough; set => _stopOnCloseEnough = value; }
}
