using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static event System.Action OnLeftMouseButtonDown = null;
    public static event System.Action OnLeftMouseButtonUp = null;
    public static event System.Action<float> OnMouseScrollSwipe = null;
    //public static event System.Action onMouseScrollUp = null;
    //public static event System.Action onMouseScrollDown = null;
    public static event System.Action OnInteractDown = null;

    private void Update()
    {
        if (Time.timeScale == Mathf.Epsilon) return;

        CheckLeftMouseButtonInput();
        CheckMouseScrollInput();
        CheckInteractInput();
    }

    private void CheckLeftMouseButtonInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftMouseButtonDown?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnLeftMouseButtonUp?.Invoke();
        }
    }

    private void CheckMouseScrollInput()
    {
        Vector2 _mouseScrollDelta = Input.mouseScrollDelta;

        if (_mouseScrollDelta.y != 0)
        {
            OnMouseScrollSwipe?.Invoke(_mouseScrollDelta.y);
        }

        //if (_mouseScrollDelta.y > 0)
        //{
        //    onMouseScrollUp?.Invoke();
        //}
        //else if (_mouseScrollDelta.y < 0)
        //{
        //    onMouseScrollDown?.Invoke();
        //}
    }

    private void CheckInteractInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteractDown?.Invoke();
        }
    }

    public static Vector3 GetMovementInput()
    {
        if (Time.timeScale == Mathf.Epsilon) return default;

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
