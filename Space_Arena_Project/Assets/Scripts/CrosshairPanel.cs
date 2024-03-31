using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairPanel : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = InputHandler.GetMousePosition();
    }
}
