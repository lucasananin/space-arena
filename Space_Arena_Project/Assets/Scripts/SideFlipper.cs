using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlipper : MonoBehaviour
{
    [SerializeField] protected Transform _transform = null;

    public void Flip(bool _toTheRight)
    {
        Vector3 _scale = Vector3.one;
        _scale.x *= _toTheRight ? 1 : -1;
        _transform.localScale = _scale;
    }
}
