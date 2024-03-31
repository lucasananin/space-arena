//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    //public static event Action<Vector2> onMoveAxis = null;

    //private void Update()
    //{
    //    SendMoveAxis();
    //}

    //private void SendMoveAxis()
    //{
    //    float _x = Input.GetAxisRaw("Horizontal");
    //    float _y = Input.GetAxisRaw("Vertical");
    //    Vector2 _moveAxis = new Vector2(_x, _y);
    //    onMoveAxis?.Invoke(_moveAxis);
    //}

    public static Vector3 GetMovementInput()
    {
        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");
        Vector2 _moveAxis = new Vector2(_x, _y);
        return _moveAxis;
    }

    private Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }
}
