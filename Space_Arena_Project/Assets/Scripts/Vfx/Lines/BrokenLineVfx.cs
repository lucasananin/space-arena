using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLineVfx : CastLineVfx
{
    [Title("// Broken")]
    [SerializeField, Range(2, 9)] int _linePoints = 4;
    [SerializeField] Vector2 _minMaxY = default;
    // randomizar o isEven.

    public void Init(Transform _target, Vector3 _hitPoint)
    {
        int _count = _lineRenderers.Length;

        for (int i = 0; i < _count; i++)
        {
            bool _startEven = Random.Range(0, 2) == 0;
            UpdateLine(_lineRenderers[i], _target, _hitPoint, _startEven);
        }

        ResetShrinkTimer();
        StartDestroyRoutine();
    }

    private void UpdateLine(LineRenderer _line, Transform _target, Vector3 _point, bool _startEven)
    {
        _line.positionCount = _linePoints;
        int _num = _startEven ? 0 : 1;

        for (int i = 0; i < _linePoints; i++)
        {
            float _t = i / ((_linePoints - 1) * 1f);
            var _position = Vector3.Lerp(transform.position, _point, _t);

            bool _isEven = _num % 2 == 0;
            _num++;

            float _yDirection = _isEven ? 1 : -1;
            float _yMultiplier = Random.Range(_minMaxY.x, _minMaxY.y);
            float _yOffset = _yDirection * _yMultiplier;

            var _cross = Vector3.Cross(_target.forward, _target.right);
            _position += _cross * _yOffset;
            //_position += _cross * _yDirection;

            _line.SetPosition(i, _position);
        }

        _line.SetPosition(0, transform.position);
        _line.SetPosition(_line.positionCount - 1, _point);
    }
}
