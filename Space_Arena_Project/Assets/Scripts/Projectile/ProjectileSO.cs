using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile_", menuName = "SO/Projectile Data")]
public class ProjectileSO : ScriptableObject
{
    [SerializeField] ProjectileBehaviour _prefab = null;

    public ProjectileBehaviour Prefab { get => _prefab; private set => _prefab = value; }
}
