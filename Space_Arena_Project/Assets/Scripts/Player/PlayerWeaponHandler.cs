using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] List<WeaponBehaviour> _weapons = null;
    [SerializeField, Range(0f, 1f)] float _changeWeaponInputDelay = 0.3f;
    
    [Title("// Debug")]
    [SerializeField, ReadOnly] WeaponBehaviour _currentWeapon = null;
    [SerializeField, ReadOnly] WeaponRotator _weaponRotator = null;
    [SerializeField, ReadOnly] int _currentWeaponIndex = 0;
    [SerializeField, ReadOnly] bool _canInputChangeWeapon = true;

    private void Awake()
    {
        ChangeCurrentWeapon();
    }

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
        if (!_canInputChangeWeapon) return;

        _currentWeaponIndex = _weapons.IndexOf(_currentWeapon);
        _currentWeaponIndex++;

        if (_currentWeaponIndex >= _weapons.Count)
            _currentWeaponIndex = 0;

        ChangeCurrentWeapon();
        StartCoroutine(ChangeWeaponDelay());
    }

    private void ChangeToPreviousWeapon()
    {
        if (!_canInputChangeWeapon) return;

        _currentWeaponIndex = _weapons.IndexOf(_currentWeapon);
        _currentWeaponIndex--;

        if (_currentWeaponIndex < 0)
            _currentWeaponIndex = _weapons.Count - 1;

        ChangeCurrentWeapon();
        StartCoroutine(ChangeWeaponDelay());
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

        //_currentWeapon.GetComponent<WeaponFlipper>().FlipToParent();
        _weaponRotator = _currentWeapon.GetComponent<WeaponRotator>();
    }

    private IEnumerator ChangeWeaponDelay()
    {
        _canInputChangeWeapon = false;
        yield return new WaitForSeconds(_changeWeaponInputDelay);
        _canInputChangeWeapon = true;
    }

    public void RotateCurrentWeapon()
    {
        _weaponRotator.LookAtMouse();
    }

    [Button]
    private void SetWeaponReference()
    {
        _currentWeapon = GetComponentInChildren<WeaponBehaviour>();
    }
}
