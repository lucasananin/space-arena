using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlipper : SideFlipper
{
    [SerializeField, ReadOnly] SideFlipper _parentFlipper = null;

    private void Awake()
    {
        _parentFlipper = transform.parent.GetComponent<SideFlipper>();
    }

    public void UpdateFlip()
    {
        //Flip(_parentFlipper.IsLookingRight(), _angle);
    }
}
