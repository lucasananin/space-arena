using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    public event Action onShoot = null;

    public void InvokeOnShootEvent()
    {
        onShoot?.Invoke();
    }

    public abstract void PullTrigger();
    public abstract void ReleaseTrigger();
}
