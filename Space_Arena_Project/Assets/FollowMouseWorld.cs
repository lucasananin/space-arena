using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseWorld : MonoBehaviour
{
    private void LateUpdate()
    {
        var _position = Camera.main.ScreenToWorldPoint(InputHandler.GetMousePosition());
        _position.z = 0f;
        transform.position = _position;
    }
}
