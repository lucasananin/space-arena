using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlipper : MonoBehaviour
{
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

        Vector3 _newEuler = Vector3.zero;

        if (_flipDataSo.FlipX)
        {
            _newEuler.x = _toTheRight ? 0 : -180;
        }

        if (_flipDataSo.FlipY)
        {
            _newEuler.y = _toTheRight ? 0 : -180;
        }

        var _absEuler = new Vector3(Mathf.Abs(_newEuler.x), Mathf.Abs(_newEuler.y), _newEuler.z);

        if (_absEuler != _target.eulerAngles)
        {
            _target.eulerAngles = _newEuler;
            OnFlip?.Invoke();
        }
    }

    public bool IsLookingRight()
    {
        return _target.eulerAngles.y == 0;
    }
}
