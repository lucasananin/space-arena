using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tags_", menuName = "SO/Tag Collection")]
public class TagCollectionSO : ScriptableObject
{
    [SerializeField] string[] _tags = null;

    public string[] Tags { get => _tags; private set => _tags = value; }
}
