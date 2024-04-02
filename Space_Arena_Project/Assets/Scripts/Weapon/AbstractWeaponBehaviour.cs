using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeaponBehaviour : MonoBehaviour
{
    public event Action onShoot = null;

    public void InvokeOnShootEvent()
    {
        onShoot?.Invoke();
        Debug.Log($"// Shoot!");
    }

    public abstract void PullTrigger();
    public abstract void ReleaseTrigger();
}
