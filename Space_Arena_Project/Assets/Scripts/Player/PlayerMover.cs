using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField, ReadOnly] Vector2 _inputDirection = default;

    private void Update()
    {
        _inputDirection = InputHandler.GetMovementInput();
    }

    //private void FixedUpdate()
    //{
    //    Move();
    //}

    public void Move()
    {
        Vector2 _velocity = _inputDirection.normalized * _moveSpeed;
        _rb.velocity = _velocity;
    }

    public void ResetVelocity()
    {
        _rb.velocity = Vector3.zero;
    }

    public bool HasInputDirection()
    {
        return _inputDirection != Vector2.zero;
    }
}
