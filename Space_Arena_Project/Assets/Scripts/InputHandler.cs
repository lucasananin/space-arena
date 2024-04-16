using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static event Action onLeftMouseButtonDown = null;
    public static event Action onLeftMouseButtonUp = null;
    public static event Action onMouseScrollUp = null;
    public static event Action onMouseScrollDown = null;

    private void Update()
    {
        CheckLeftMouseButtonInput();
        CheckMouseScrollInput();
    }

    private void CheckLeftMouseButtonInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onLeftMouseButtonDown?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            onLeftMouseButtonUp?.Invoke();
        }
    }

    private void CheckMouseScrollInput()
    {
        Vector2 _mouseScrollDelta = Input.mouseScrollDelta;

        if (_mouseScrollDelta.y > 0)
        {
            onMouseScrollUp?.Invoke();
        }
        else if (_mouseScrollDelta.y < 0)
        {
            onMouseScrollDown?.Invoke();
        }
    }

    public static Vector3 GetMovementInput()
    {
        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");
        Vector2 _moveAxis = new Vector2(_x, _y);
        return _moveAxis;
    }

    public static Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }
}
