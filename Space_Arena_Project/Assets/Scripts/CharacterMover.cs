using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField, ReadOnly] Vector2 _moveDirection = default;

    //private void OnEnable()
    //{
    //    InputHandler.onMoveAxis += SetMoveDirection;
    //}

    //private void OnDisable()
    //{
    //    InputHandler.onMoveAxis -= SetMoveDirection;
    //}

    private void Update()
    {
        //SetMoveDirection(InputHandler.GetMovementInput());
        _moveDirection = InputHandler.GetMovementInput();
    }

    private void FixedUpdate()
    {
        Vector2 _velocity = _moveDirection.normalized * _moveSpeed;
        _rb.velocity = _velocity;
    }

    //private void SetMoveDirection(Vector2 _moveAxis)
    //{
    //    _moveDirection = _moveAxis;
    //}
}
