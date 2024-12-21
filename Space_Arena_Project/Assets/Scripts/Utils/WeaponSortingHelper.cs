using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSortingHelper : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] float _minRightAngle = 26f;

    private void OnValidate()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        var _z = transform.localRotation.eulerAngles.z;
        var _isAboveTheLimit = _z > _minRightAngle && _z < 90f;
        _renderer.sortingOrder = _isAboveTheLimit ? -1 : 0;
    }
}
