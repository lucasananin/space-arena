using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlipper : SideFlipper
{
    [SerializeField] SideFlipper _parentFlipper = null;
    //[SerializeField] protected FlipModel _aimFlipModel = null;
    //[SerializeField] protected FlipModel _movementFlipModel = null;
    //[SerializeField] bool _isAiming = false;

    [SerializeField] bool _useAimFlipData = true;
    [SerializeField] FlipDataSO _aimFlipDataSo = null;
    [SerializeField] FlipDataSO _movementFlipDataSo = null;
    [SerializeField, ReadOnly] Vector3 _right = default;

    // criar um SOs com os parametros.
    // flip_xOnly.
    // flip_yOnly.

    private void OnEnable()
    {
        _parentFlipper.onFlip += FlipToParent;
    }

    private void OnDisable()
    {
        _parentFlipper.onFlip -= FlipToParent;
    }

    private void Update()
    {
        //Debug.Log($"// {transform.right}");
        //if (transform.right.x > 0)
        //{
        //    ChangeToMovementFlipData();
        //    //FlipToParent();
        //}
        //else if (transform.right.x < 0)
        //{
        //    ChangeToAimFlipData();
        //}

        //bool _isParentFacingRight = _parentFlipper.IsLookingRight();
        //bool _isRotatedRight = transform.right.x > 0;
        //bool _isRotatedLeft = transform.right.x < 0;

        //if (_isParentFacingRight && _isRotatedRight)
        //{
        //    //Flip(true);
        //    //FlipY(false);
        //    Flip(1, 1);
        //}
        //else if (!_isParentFacingRight && _isRotatedLeft)
        //{
        //    //Flip(true);
        //    //FlipY(true);
        //    Flip(-1, -1);
        //}

        //FlipY(_parentFlipper.IsLookingRight());
        _right = transform.right;
    }

    private void LateUpdate()
    {
        //return;
        bool _isParentFacingRight = _parentFlipper.IsLookingRight();
        bool _isRotatedRight = transform.right.x > 0;
        bool _isRotatedLeft = transform.right.x < 0;

        if (_isParentFacingRight && _isRotatedRight)
        {
            Flip(1, 1);
        }
        else if (!_isParentFacingRight && _isRotatedLeft)
        {
            Flip(1, -1);
        }
        else if (_isParentFacingRight && _isRotatedLeft)
        {
            Flip(-1, -1);
        }
        else if (!_isParentFacingRight && _isRotatedRight)
        {
            Flip(-1, 1);
        }
    }

    public void FlipToParent()
    {
        //Flip(_parentFlipper.IsLookingRight());

        //UpdateFlipDataSo();
        //if (transform.right.x > 0)
        //{
        //    ChangeToAimFlipData();
        //}
        //else if (transform.right.x < 0)
        //{
        //    ChangeToMovementFlipData();
        //}

        //bool _isParentFacingRight = _parentFlipper.IsLookingRight();
        //bool _isRotatedRight = transform.right.x > 0;
        //bool _isRotatedLeft = transform.right.x < 0;

        //if (_isParentFacingRight && _isRotatedRight)
        //{
        //    Flip(1, 1);
        //}
        //else if (!_isParentFacingRight && _isRotatedLeft)
        //{
        //    Flip(-1, -1);
        //}
    }

    private void UpdateFlipDataSo()
    {
        var _z = transform.localEulerAngles.z;
        if (IsLookingRight())
        {
            if (_z <= -90 || _z >= 90)
            {

            }
        }
        else
        {

        }

        if (_useAimFlipData)
        {
            _flipDataSo = _aimFlipDataSo;
        }
        else
        {
            _flipDataSo = _movementFlipDataSo;
        }
    }

    public void ChangeToAimFlipData()
    {
        _useAimFlipData = true;
        _flipDataSo = _aimFlipDataSo;
    }

    public void ChangeToMovementFlipData()
    {
        _useAimFlipData = false;
        _flipDataSo = _movementFlipDataSo;
    }
}

//[System.Serializable]
//public class FlipModel
//{
//    public bool flipX = false;
//    public bool flipY = false;
//}
