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
        var _rotation = GeneralMethods.GetLookRotation(transform.position, _target);
        SetRotation(_rotation);
    }

    public void LookAtPosition(Vector3 _position)
    {
        if (!CanRotate()) return;

        var _rotation = GeneralMethods.GetLookRotation(transform.position, _position);
        SetRotation(_rotation);
    }

    public void ResetRotation()
    {
        SetRotation(Quaternion.identity);
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
