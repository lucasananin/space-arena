using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSortingHelper : MonoBehaviour
{
    [SerializeField] WeaponFlipper _flipper = null;
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] float _minRightAngle = 26f;
    [SerializeField] float _minLeftAngle = 206f;
    //[SerializeField] Vector2 _minMax = default;
    //[SerializeField, ReadOnly] float _zRotation = 0f;

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
