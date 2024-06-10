using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlipper : SideFlipper
{
    //[SerializeField] WeaponBehaviour _weaponBehaviour = null;
    [SerializeField, ReadOnly] SideFlipper _parentFlipper = null;

    private void Awake()
    {
        _parentFlipper = transform.parent.GetComponent<SideFlipper>();
    }

    //private void OnEnable()
    //{
    //    //_parentFlipper.onFlip += FlipToParent;
    //    //_parentFlipper.onFlip += UpdateFlip;
    //    _weaponBehaviour.onInit += SetParentFlipper;
    //}

    //private void OnDisable()
    //{
    //    //_parentFlipper.onFlip -= FlipToParent;
    //    //_parentFlipper.onFlip -= UpdateFlip;
    //    _weaponBehaviour.onInit -= SetParentFlipper;
    //}

    //private void FixedUpdate()
    //{
    //    UpdateFlip();
    //}

    //public void FlipToParent()
    //{
    //    Flip(_parentFlipper.IsLookingRight());
    //}

    public void UpdateFlip()
    {
        Flip(_parentFlipper.IsLookingRight());
        //CheckFlip();
    }

    private void CheckFlip()
    {
        Vector3 _rightDirection = transform.right;
        bool _isParentFacingRight = _parentFlipper.IsLookingRight();
        bool _isParentFacingLeft = !_parentFlipper.IsLookingRight();
        bool _isRotatedRight = _rightDirection.x >= 0;
        bool _isRotatedLeft = _rightDirection.x < 0;

        //if (_rightDirection == Vector3.zero)
        //{
        //    Debug.Log($"// zero");
        //}

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
        else
        {
            Flip(_parentFlipper.IsLookingRight());
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

    //private void SetParentFlipper(WeaponBehaviour _weapon)
    //{
    //    if (_parentFlipper == null)
    //    {
    //        _parentFlipper = _weapon.EntitySource.GetComponent<SideFlipper>();
    //    }
    //}
}
