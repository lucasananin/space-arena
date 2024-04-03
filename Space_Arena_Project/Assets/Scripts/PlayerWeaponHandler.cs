using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _currentWeapon = null;

    private void OnEnable()
    {
        InputHandler.onFireInputDown += PullTrigger;
        InputHandler.onFireInputUp += ReleaseTrigger;
    }

    private void OnDisable()
    {
        InputHandler.onFireInputDown -= PullTrigger;
        InputHandler.onFireInputUp -= ReleaseTrigger;
    }

    private void PullTrigger()
    {
        _currentWeapon.PullTrigger();
    }

    private void ReleaseTrigger()
    {
        _currentWeapon.ReleaseTrigger();
    }

    [Button]
    private void SetWeaponReference()
    {
        _currentWeapon = GetComponentInChildren<WeaponBehaviour>();
    }
}
