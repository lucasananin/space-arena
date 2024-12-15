using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlipper : MonoBehaviour
{
    // mudar o nome dessa variavel para _transformToFlip
    [SerializeField] protected Transform _target = null;
    [SerializeField] protected FlipDataSO _flipDataSo = null;
    [SerializeField] protected bool _canFlip = true;

    public event System.Action OnFlip = null;

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

    public void Flip(bool _toTheRight)
    {
        if (!_canFlip) return;

        Vector3 _newScale = Vector3.one;
        //Vector3 _newScale = Vector3.zero;

        if (_flipDataSo.FlipX)
        {
            _newScale.x *= _toTheRight ? 1 : -1;
            //_newScale.x = _toTheRight ? 0 : -180;
        }

        if (_flipDataSo.FlipY)
        {
            _newScale.y *= _toTheRight ? 1 : -1;
            //_newScale.y = _toTheRight ? 0 : -180;
        }

        if (_newScale != _target.localScale)
        {
            _target.localScale = _newScale;
            //_target.eulerAngles = _newScale;
            OnFlip?.Invoke();
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
            OnFlip?.Invoke();
        }
    }

    public void ResetFlip()
    {
        ForceFlip(1, 1);
    }

    public bool IsLookingRight()
    {
        return _target.localScale.x >= 0;
        //return _target.eulerAngles.y == 0;
    }
}
