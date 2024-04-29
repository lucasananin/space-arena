using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity_", menuName = "SO/Entities/AI Entity")]
public class AiEntitySO : EntitySO
{
    [SerializeField, Range(0f, 99f)] float _shootDistance = 0f;

    public float ShootDistance { get => _shootDistance; private set => _shootDistance = value; }
}
