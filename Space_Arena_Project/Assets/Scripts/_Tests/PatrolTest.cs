using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTest : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] Vector3[] _targetPositions = null;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField, Range(0.01f, 0.5f)] float _stoppingDistance = 0.1f;
    [SerializeField, ReadOnly] int _currentTargetIndex = 0;

    private void Awake()
    {
        transform.position = _targetPositions[0];
        _currentTargetIndex = 1;
    }

    private void Update()
    {
        Vector3 _moveDirection = (_targetPositions[_currentTargetIndex]- transform.position).normalized;
        Vector3 _velocity = _moveDirection * _moveSpeed;
        _rb.velocity = _velocity;

        float _distance = Vector2.Distance(transform.position, _targetPositions[_currentTargetIndex]);

        if (_distance < _stoppingDistance)
        {
            _currentTargetIndex++;

            if (_currentTargetIndex >= _targetPositions.Length)
            {
                _currentTargetIndex = 0;
            }
        }
    }
}
