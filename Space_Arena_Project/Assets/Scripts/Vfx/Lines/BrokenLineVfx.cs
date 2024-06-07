using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLineVfx : LaserLineVfx
{
    [SerializeField, Range(2, 9)] int _linePoints = 4;

    public void Init(Transform _target, Vector3 _hitPoint)
    {
        int _count = _lineRenderers.Length;

        for (int i = 0; i < _count; i++)
        {
            UpdateLine(_lineRenderers[i], _target, _hitPoint);
        }

        ResetShrinkTimer();
        StartDestroyRoutine();
    }

    private void UpdateLine(LineRenderer _line, Transform _target, Vector3 _point)
    {
        _line.positionCount = _linePoints;

        for (int i = 0; i < _linePoints; i++)
        {
            float _t = i / ((_linePoints - 1) * 1f);
            var _position = Vector3.Lerp(transform.position, _point, _t);

            bool _isEven = i % 2 == 0;
            float _yDirection = _isEven ? 1 : -1;
            //_position.y += Random.Range(_minMaxY.x, _minMaxY.y) * _yDirection;

            var _cross = Vector3.Cross(_target.forward, _target.right);
            _position += _cross * _yDirection;

            _line.SetPosition(i, _position);
        }

        _line.SetPosition(0, transform.position);
        _line.SetPosition(_line.positionCount - 1, _point);
    }
}
