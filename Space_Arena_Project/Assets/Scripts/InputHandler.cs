using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
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
