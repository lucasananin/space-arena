using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] bool _canRotate = true;
    //[SerializeField] bool _smoothRotation = false;
    //[SerializeField] float _rotationSpeed = 0f;

    public void LookAtMouse()
    {
        if (!CanRotate()) return;

        //Vector3 _mousePosition = InputHandler.GetMousePosition();
        //Vector3 _direction = _mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        //float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        //Quaternion _rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        var _target = Camera.main.ScreenToWorldPoint(InputHandler.GetMousePosition());
        var _rotation = GeneralMethods.GetLookRotation(transform.position, _target);
        SetRotation(_rotation);
    }

    public void LookAtPosition(Vector3 _position)
    {
        if (!CanRotate()) return;

        //Vector3 _direction = _targetPosition - transform.position;
        //float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        //Quaternion _rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        var _rotation = GeneralMethods.GetLookRotation(transform.position, _position);
        SetRotation(_rotation);
    }

    //public void LookAtDirection(Vector3 _direction)
    //{
    //    if (!CanRotate()) return;

    //    float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
    //    Quaternion _rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    //    SetRotation(_rotation);
    //}

    public void ResetRotation()
    {
        SetRotation(Quaternion.identity);
    }

    private void SetRotation(Quaternion _rotation)
    {
        //if (_smoothRotation)
        //    transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, _rotationSpeed * Time.deltaTime);
        //else
        //    transform.rotation = _rotation;

        transform.rotation = _rotation;
    }

    private bool CanRotate()
    {
        if (!_canRotate)
            ResetRotation();

        return _canRotate;
    }
}
