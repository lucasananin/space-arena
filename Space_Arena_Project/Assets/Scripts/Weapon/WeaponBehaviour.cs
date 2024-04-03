using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject _characterSource = null;
    [SerializeField] protected WeaponSO _weaponSO = null;

    public event Action onShoot = null;

    public void InvokeOnShootEvent()
    {
        onShoot?.Invoke();
        Debug.Log($"// Shoot!");
    }

    public abstract void PullTrigger();
    public abstract void ReleaseTrigger();
}
