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

        //Vector3 _newScale = Vector3.one;
        Vector3 _newEuler = Vector3.zero;

        if (_flipDataSo.FlipX)
        {
            //_newScale.x *= _toTheRight ? 1 : -1;
            _newEuler.x = _toTheRight ? 0 : -180;
        }

        if (_flipDataSo.FlipY)
        {
            //_newScale.y *= _toTheRight ? 1 : -1;
            _newEuler.y = _toTheRight ? 0 : -180;
        }

        var _absEuler = new Vector3(Mathf.Abs(_newEuler.x), Mathf.Abs(_newEuler.y), _newEuler.z);

        //if (_newScale != _target.localScale)
        if (_absEuler != _target.eulerAngles)
        {
            //_target.localScale = _newScale;
            _target.eulerAngles = _newEuler;
            OnFlip?.Invoke();
            Debug.Log($"// onFlip", this);
        }
    }

    //public void Flip(bool _toTheRight, float _z)
    //{
    //    if (!_canFlip) return;

    //    Vector3 _newScale = Vector3.zero;

    //    if (_flipDataSo.FlipX)
    //    {
    //        _newScale.x = _toTheRight ? 0 : -180;
    //    }

    //    if (_flipDataSo.FlipY)
    //    {
    //        _newScale.y = _toTheRight ? 0 : -180;
    //    }

    //    _newScale.z = _z;

    //    if (_newScale != _target.localScale)
    //    {
    //        _target.eulerAngles = _newScale;
    //        OnFlip?.Invoke();
    //    }
    //}

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
        //return _target.localScale.x >= 0;
        return _target.eulerAngles.y == 0;
    }
}
