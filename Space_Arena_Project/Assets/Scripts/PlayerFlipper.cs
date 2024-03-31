using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipper : SideFlipper
{
    private void LateUpdate()
    {
        Vector3 _worldMousePosition = Camera.main.ScreenToWorldPoint(InputHandler.GetMousePosition());
        bool _toTheRight = _worldMousePosition.x >= _transform.position.x;
        Flip(_toTheRight);
    }
}
