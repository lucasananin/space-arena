using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] bool _canRotate = true;
    [SerializeField] bool _smoothRotation = false;
    [SerializeField] float _rotationSpeed = 10f;

    public void LookAtMouse()
    {
        if (_canRotate)
        {
            Vector3 _mousePosition = InputHandler.GetMousePosition();
            Vector3 _direction = _mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            Quaternion _rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            SetRotation(_rotation);
        }
        else
        {
            ResetRotation();
        }
    }

    public void LookAt(Vector3 _targetPosition)
    {
        if (_canRotate)
        {
            Vector3 _direction = _targetPosition - transform.position;
            float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            Quaternion _rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            SetRotation(_rotation);
        }
        else
        {
            ResetRotation();
        }
    }

    public void ResetRotation()
    {
        SetRotation(Quaternion.identity);
    }

    private void SetRotation(Quaternion _rotation)
    {
        if (_smoothRotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, _rotationSpeed * Time.deltaTime);
        else
            transform.rotation = _rotation;
    }
}
