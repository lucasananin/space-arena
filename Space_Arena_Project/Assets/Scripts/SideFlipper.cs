using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlipper : MonoBehaviour
{
    // mudar o nome dessa variavel para _transformToFlip
    [SerializeField] protected Transform _target = null;
    [SerializeField] protected FlipDataSO _flipDataSo = null;
    [SerializeField] protected bool _canFlip = true;
    //[SerializeField] protected bool _flipX = true;
    //[SerializeField] protected bool _flipY = false;

    public event System.Action onFlip = null;

    public void FlipByCompareX(float _x1, float _x2)
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

    //public void Flip(bool _toTheRight)
    //{
    //    Flip(_toTheRight, _flipDataSo.FlipX, _flipDataSo.FlipY);
    //}

    public void Flip(bool _toTheRight/*, bool _flipX, bool _flipY*/)
    {
        if (!_canFlip) return;

        Vector3 _newScale = Vector3.one;

        if (_flipDataSo.FlipX)
            _newScale.x *= _toTheRight ? 1 : -1;

        if (_flipDataSo.FlipY)
            _newScale.y *= _toTheRight ? 1 : -1;

        //if (_flipX)
        //    _newScale.x *= _toTheRight ? 1 : -1;

        //if (_flipY)
        //    _newScale.y *= _toTheRight ? 1 : -1;

        if (_newScale != _target.localScale)
        {
            _target.localScale = _newScale;
            onFlip?.Invoke();
        }
    }

    public void ForceFlip(float _x, float _y)
    {
        Vector3 _newScale = Vector3.one;
        _newScale.x *= _x;
        _newScale.y *= _y;

        if (_newScale != _target.localScale)
        {
            _target.localScale = _newScale;
            onFlip?.Invoke();
        }
    }

    public void ResetFlip()
    {
        ForceFlip(1, 1);
    }

    public bool IsLookingRight()
    {
        return _target.localScale.x >= 0;
    }
}
