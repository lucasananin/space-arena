using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] List<WeaponBehaviour> _weapons = null;
    [SerializeField, ReadOnly] WeaponBehaviour _currentWeapon = null;
    [SerializeField, ReadOnly] int _currentWeaponIndex = 0;

    public WeaponBehaviour CurrentWeapon { get => _currentWeapon; private set => _currentWeapon = value; }

    private void OnEnable()
    {
        InputHandler.onLeftMouseButtonDown += PullTrigger;
        InputHandler.onLeftMouseButtonUp += ReleaseTrigger;
        InputHandler.onMouseScrollUp += ChangeToNextWeapon;
        InputHandler.onMouseScrollDown += ChangeToPreviousWeapon;
    }

    private void OnDisable()
    {
        InputHandler.onLeftMouseButtonDown -= PullTrigger;
        InputHandler.onLeftMouseButtonUp -= ReleaseTrigger;
        InputHandler.onMouseScrollUp -= ChangeToNextWeapon;
        InputHandler.onMouseScrollDown -= ChangeToPreviousWeapon;
    }

    private void PullTrigger()
    {
        _currentWeapon.PullTrigger();
    }

    private void ReleaseTrigger()
    {
        _currentWeapon.ReleaseTrigger();
    }

    private void ChangeToNextWeapon()
    {
        _currentWeaponIndex = _weapons.IndexOf(_currentWeapon);
        _currentWeaponIndex++;

        if (_currentWeaponIndex >= _weapons.Count)
            _currentWeaponIndex = 0;

        ChangeCurrentWeapon();
    }

    private void ChangeToPreviousWeapon()
    {
        _currentWeaponIndex = _weapons.IndexOf(_currentWeapon);
        _currentWeaponIndex--;

        if (_currentWeaponIndex < 0)
            _currentWeaponIndex = _weapons.Count - 1;

        ChangeCurrentWeapon();
    }

    private void ChangeCurrentWeapon()
    {
        _currentWeapon = _weapons[_currentWeaponIndex];

        int _count = _weapons.Count;

        for (int i = 0; i < _count; i++)
        {
            bool _isCurrentWeapon = i == _currentWeaponIndex;
            _weapons[i].gameObject.SetActive(_isCurrentWeapon);
        }

        _currentWeapon.GetComponent<WeaponFlipper>().FlipToParent();
    }

    public void RotateCurrentWeapon()
    {
        _currentWeapon.GetComponent<WeaponRotator>().LookAtMouse();
    }

    [Button]
    private void SetWeaponReference()
    {
        _currentWeapon = GetComponentInChildren<WeaponBehaviour>();
    }
}
