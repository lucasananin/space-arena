using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] bool _canRotate = true;

    public void LookAtMouse()
    {
        if (_canRotate)
        {
            Vector3 _mousePosition = InputHandler.GetMousePosition();
            Vector3 _direction = _mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void LookAt(Vector3 _targetPosition)
    {
        Vector3 _direction = _targetPosition - transform.position;
        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    }
}
