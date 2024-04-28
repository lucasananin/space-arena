using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flip_", menuName = "SO/Flip Data")]
public class FlipDataSO : ScriptableObject
{
    [SerializeField] FlipData _flipData = null;

    public bool FlipX => _flipData.flipX;
    public bool FlipY => _flipData.flipY;
}

[System.Serializable]
public class FlipData
{
    public bool flipX = false;
    public bool flipY = false;
}