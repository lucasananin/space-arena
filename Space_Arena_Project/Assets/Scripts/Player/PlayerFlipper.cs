using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipper : SideFlipper
{
    //private void Update()
    //{
    //    FlipToMouse();
    //}

    public void FlipToMouse()
    {
        Vector3 _worldMousePosition = Camera.main.ScreenToWorldPoint(InputHandler.GetMousePosition());
        bool _toTheRight = _worldMousePosition.x >= _target.position.x;
        Flip(_toTheRight);
    }
}
