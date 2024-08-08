using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbRotateFx : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] Transform _transform = null;
    [SerializeField] float _twistsPerSecond = 1f;
    [SerializeField] float _defaultMagnitude = 20f;

    private void Update()
    {
        var _euler = GetRotationSpeed() * Time.deltaTime * Vector3.forward;
        _transform.Rotate(_euler);
    }

    private float GetRotationSpeed()
    {
        return _twistsPerSecond * 360f * (_rb.velocity.magnitude / _defaultMagnitude);
    }
}
