using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotationHandler : MonoBehaviour
{
    [SerializeField] List<WeaponRotator> _rotators = null;
    [SerializeField] List<WeaponFlipper> _flippers = null;

    public void RotateWeapons(Vector3 _position)
    {
        int _count = _rotators.Count;

        for (int i = 0; i < _count; i++)
        {
            _rotators[i].LookAtPosition(_position);
            _flippers[i].UpdateFlip();
        }
    }

    public void ResetWeaponRotations()
    {
        int _count = _rotators.Count;

        for (int i = 0; i < _count; i++)
        {
            _rotators[i].ResetRotation();
            _flippers[i].ResetFlip();
        }
    }
}
