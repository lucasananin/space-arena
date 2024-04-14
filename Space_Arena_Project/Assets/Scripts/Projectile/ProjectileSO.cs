using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile_", menuName = "SO/Combat/Projectile Data")]
public class ProjectileSO : ScriptableObject
{
    [SerializeField] ProjectileBehaviour _prefab = null;
    [SerializeField] protected Vector2 _spawnPositionOffset = default;

    public ProjectileBehaviour Prefab { get => _prefab; private set => _prefab = value; }
    public Vector2 SpawnPositionOffset { get => _spawnPositionOffset; private set => _spawnPositionOffset = value; }
}
