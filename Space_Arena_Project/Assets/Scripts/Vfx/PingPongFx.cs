using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongFx : MonoBehaviour
{
    [SerializeField] Transform _transform = null;
    [SerializeField] bool _useSin = true;
    [SerializeField, Range(0f, 9f)] float _speed = 1f;
    [SerializeField, Range(0f, 2f)] float _yMagnitude = 1f;
    [SerializeField, Range(0f, 2f)] float _xMagnitude = 1f;

    private void Update()
    {
        float _y = _useSin ? GetSin(_yMagnitude) : GetCos(_yMagnitude);
        float _x = _useSin ? GetSin(_xMagnitude) : GetCos(_xMagnitude);
        Vector3 _position = new Vector3(_x, _y);
        _transform.localPosition = _position;
    }

    private float GetSin(float _magnitude)
    {
        return Mathf.Sin(Time.time * _speed) * _magnitude;
    }

    private float GetCos(float _magnitude)
    {
        return Mathf.Cos(Time.time * _speed) * _magnitude;
    }
}
