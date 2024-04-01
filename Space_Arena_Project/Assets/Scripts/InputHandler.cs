using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static event Action onFireInputDown = null;
    public static event Action onFireInputUp = null;

    private void Update()
    {
        CheckFireInput();
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

    public void CheckFireInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onFireInputDown?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            onFireInputUp?.Invoke();
        }
    }
}
