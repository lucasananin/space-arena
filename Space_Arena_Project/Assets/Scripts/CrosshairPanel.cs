using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairPanel : MonoBehaviour
{
    private void LateUpdate()
    {
        //var _position = Camera.main.WorldToScreenPoint(InputHandler.GetMousePosition());
        var _position = Camera.main.ScreenToWorldPoint(InputHandler.GetMousePosition());
        _position.z = 0f;
        transform.position = _position;

        //transform.position = InputHandler.GetMousePosition();
    }
}
