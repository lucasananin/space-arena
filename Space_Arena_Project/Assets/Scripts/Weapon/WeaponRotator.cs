using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] bool _canRotate = true;

    public void LookAtMouse()
    {
        if (!CanRotate()) return;

        var _target = Camera.main.ScreenToWorldPoint(InputHandler.GetMousePosition());
        var _direction = transform.parent.InverseTransformPoint(_target);
        var _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, _angle);
    }

    public void LookAtPosition(Vector3 _position)
    {
        if (!CanRotate()) return;

        var _target = _position;
        var _direction = transform.parent.InverseTransformPoint(_target);
        var _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, _angle);
    }

    public void ResetRotation()
    {
        var _target = transform.position + new Vector3(transform.parent.right.x, 0, 0);
        var _direction = transform.parent.InverseTransformPoint(_target);
        var _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, _angle);
    }

    private void SetRotation(Quaternion _rotation)
    {
        transform.rotation = _rotation;
    }

    private bool CanRotate()
    {
        if (!_canRotate)
            ResetRotation();

        return _canRotate;
    }
}
