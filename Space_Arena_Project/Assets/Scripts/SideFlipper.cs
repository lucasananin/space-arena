using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlipper : MonoBehaviour
{
    [SerializeField] protected Transform _target = null;
    [SerializeField] protected bool _flipX = true;
    [SerializeField] protected bool _flipY = false;

    public event Action onFlip = null;

    public void Flip(bool _toTheRight)
    {
        Vector3 _newScale = Vector3.one;

        if (_flipX)
            _newScale.x *= _toTheRight ? 1 : -1;

        if (_flipY)
            _newScale.y *= _toTheRight ? 1 : -1;

        if (_newScale != _target.localScale)
        {
            _target.localScale = _newScale;
            onFlip?.Invoke();
        }
    }

    public void FlipToX(float _x1, float _x2)
    {
        if (_x1 > _x2)
        {
            Flip(true);
        }
        else if (_x1 < _x2)
        {
            Flip(false);
        }
    }

    public bool IsLookingRight()
    {
        return _target.localScale.x >= 0;
    }
}
