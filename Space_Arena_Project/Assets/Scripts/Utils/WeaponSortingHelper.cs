using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSortingHelper : MonoBehaviour
{
    [SerializeField] WeaponFlipper _flipper = null;
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] float _minRightAngle = 26f;
    [SerializeField, ReadOnly] float _minLeftAngle = 206f;
    //[SerializeField, ReadOnly] float _zRotation = 0f;

    private void OnValidate()
    {
        _flipper = GetComponent<WeaponFlipper>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _minLeftAngle = +_minRightAngle + 180;
    }

    private void LateUpdate()
    {
        var _z = transform.localRotation.eulerAngles.z;
        var _isFacingRight = _flipper.IsLookingRight();
        var _r = _isFacingRight && _z > _minRightAngle && _z < 90f;
        var _l = !_isFacingRight && _z > _minLeftAngle && _z < 270f;
        var _isAboveTheLimit = _r || _l;
        _renderer.sortingOrder = _isAboveTheLimit ? -1 : 0;
        //_zRotation = _z;
    }
}
