using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlipper : SideFlipper
{
    [SerializeField] SideFlipper _parentFlipper = null;

    private void OnEnable()
    {
        _parentFlipper.onFlip += FlipToParent;
    }

    private void OnDisable()
    {
        _parentFlipper.onFlip -= FlipToParent;
    }

    private void FlipToParent()
    {
        Flip(_parentFlipper.IsLookingRight());
    }
}
