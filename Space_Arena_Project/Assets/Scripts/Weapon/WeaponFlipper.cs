using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlipper : SideFlipper
{
    [SerializeField] SideFlipper _parentFlipper = null;

    //private void OnEnable()
    //{
    //    _parentFlipper.onFlip += FlipToParent;
    //}

    //private void OnDisable()
    //{
    //    _parentFlipper.onFlip -= FlipToParent;
    //}

    private void LateUpdate()
    {
        UpdateFlip();
    }

    //public void FlipToParent()
    //{
    //    Flip(_parentFlipper.IsLookingRight());
    //}

    private void UpdateFlip()
    {
        //return;
        Vector3 _rightDirection = transform.right;
        bool _isParentFacingRight = _parentFlipper.IsLookingRight();
        bool _isParentFacingLeft = !_parentFlipper.IsLookingRight();
        bool _isRotatedRight = _rightDirection.x > 0;
        bool _isRotatedLeft = _rightDirection.x < 0;

        if (_isParentFacingRight && _isRotatedRight)
        {
            //ForceFlip(1, 1);
            ForceFlip(1, 1);
        }
        else if (_isParentFacingLeft && _isRotatedLeft)
        {
            //ForceFlip(1, -1);
            ForceFlip(-1, -1);
        }
        //else if (_isParentFacingLeft && _isRotatedRight)
        //{
        //    ForceFlip(-1, 1);
        //}
        //else if (_isParentFacingRight && _isRotatedLeft)
        //{
        //    ForceFlip(-1, -1);
        //}
    }
}
