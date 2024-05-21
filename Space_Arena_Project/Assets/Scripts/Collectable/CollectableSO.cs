using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableSO : ScriptableObject
{
    [SerializeField] CollectableBehaviour _prefab = null;

    public CollectableBehaviour Prefab { get => _prefab; private set => _prefab = value; }

    public abstract void Collect(CollectableAgent _agent);
}
