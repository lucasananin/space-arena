using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLineVfx : CastLineVfx
{
    [Title("// Broken")]
    [SerializeField] BrokeLineMode _mode = default;
    [SerializeField] Vector2 _minMaxYOffset = default;
    [SerializeField] bool _randomizePointsCount = false;
    //[SerializeField, Range(2, 9)] int _linePoints = 4;
    [SerializeField] Vector2 _minMaxPointsCount = default;
    [SerializeField, ReadOnly] int _totalPoints = 0;

    public void Init(Transform _target, Vector3 _hitPoint)
    {
        int _count = _lineRenderers.Length;

        bool _startEven = Random.Range(0, 2) == 0;
        int _num = _startEven ? 0 : 1;

        for (int i = 0; i < _count; i++)
        {
            switch (_mode)
            {
                case BrokeLineMode.SameDirection:
                    UpdateLine(_lineRenderers[i], _target, _hitPoint, _startEven);
                    break;

                case BrokeLineMode.AlwaysOpposite:
                    bool _isEven = _num % 2 == 0;
                    _num++;
                    UpdateLine(_lineRenderers[i], _target, _hitPoint, _isEven);
                    break;

                case BrokeLineMode.Random:
                    _startEven = Random.Range(0, 2) == 0;
                    UpdateLine(_lineRenderers[i], _target, _hitPoint, _startEven);
                    break;

                default:
                    break;
            }
        }

        ResetShrinkTimer();
        StartDestroyRoutine();
    }

    private void UpdateLine(LineRenderer _line, Transform _target, Vector3 _point, bool _startEven)
    {
        var _initialPosition = transform.position;
        _totalPoints = GetPositionCount(_initialPosition, _point);
        _line.positionCount = _totalPoints;
        int _num = _startEven ? 0 : 1;

        for (int i = 0; i < _totalPoints; i++)
        {
            float _t = i / ((_totalPoints - 1) * 1f);
            var _position = Vector3.Lerp(_initialPosition, _point, _t);

            bool _isEven = _num % 2 == 0;
            _num++;

            float _yDirection = _isEven ? 1 : -1;
            float _randomYOffset = Random.Range(_minMaxYOffset.x, _minMaxYOffset.y);
            float _yOffset = _yDirection * _randomYOffset;

            var _cross = Vector3.Cross(_target.forward, _target.right);
            _position += _cross * _yOffset;

            _line.SetPosition(i, _position);
        }

        _line.SetPosition(0, _initialPosition);
        _line.SetPosition(_line.positionCount - 1, _point);
    }

    private int GetPositionCount(Vector3 _initialPosition, Vector3 _point)
    {
        if (_randomizePointsCount)
        {
            var _distance = Vector2.Distance(_initialPosition, _point);
            var _randomPointCount = Mathf.RoundToInt(_distance) + Random.Range(-2, 3);
            return Mathf.Clamp(_randomPointCount, 3, 99);
        }
        else
        {
            var _randomPointCount = Random.Range(_minMaxPointsCount.x, _minMaxPointsCount.y);
            return Mathf.RoundToInt(_randomPointCount);
            //_totalPoints = _linePoints;
        }
    }
}

public enum BrokeLineMode
{
    SameDirection,
    AlwaysOpposite,
    Random,
}