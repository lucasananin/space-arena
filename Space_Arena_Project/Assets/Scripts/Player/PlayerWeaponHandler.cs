using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] EntityBehaviour _entitySource = null;
    [SerializeField, Range(1, 9)] int _maxWeaponsCount = 2;
    [SerializeField, Range(0f, 1f)] float _changeWeaponInputDelay = 0.3f;
    [SerializeField] List<WeaponBehaviour> _weaponsList = null;

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
        _currentWeapon?.PullTrigger();
    }

    private void ReleaseTrigger()
    {
        _currentWeapon?.ReleaseTrigger();
    }

    private void ChangeToNextWeapon()
    {
        //if (!_canInputChangeWeapon) return;

        //_currentWeaponIndex = _weaponsList.IndexOf(_currentWeapon);
        //_currentWeaponIndex++;

        //if (_currentWeaponIndex >= _weaponsList.Count)
        //    _currentWeaponIndex = 0;

        //ChangeCurrentWeapon();
        //StartCoroutine(ChangeWeaponDelay());
        ChangeWeaponIndex(1);
    }

    private void ChangeToPreviousWeapon()
    {
        //if (!_canInputChangeWeapon) return;

        //_currentWeaponIndex = _weaponsList.IndexOf(_currentWeapon);
        //_currentWeaponIndex--;

        //if (_currentWeaponIndex < 0)
        //    _currentWeaponIndex = _weaponsList.Count - 1;

        //ChangeCurrentWeapon();
        //StartCoroutine(ChangeWeaponDelay());
        ChangeWeaponIndex(-1);
    }

    private void ChangeWeaponIndex(int _value)
    {
        if (!_canInputChangeWeapon) return;

        _currentWeaponIndex = _weaponsList.IndexOf(_currentWeapon);
        _currentWeaponIndex += _value;

        if (_currentWeaponIndex >= _weaponsList.Count)
            _currentWeaponIndex = 0;

        if (_currentWeaponIndex < 0)
            _currentWeaponIndex = _weaponsList.Count - 1;

        ChangeCurrentWeapon();
        StartCoroutine(ChangeWeaponDelay());
    }

    private void ChangeCurrentWeapon()
    {
        if (_weaponsList.Count <= 0) return;

        _currentWeapon = _weaponsList[_currentWeaponIndex];
        _currentWeapon.Init(_entitySource);

        int _count = _weaponsList.Count;

        for (int i = 0; i < _count; i++)
        {
            bool _isCurrentWeapon = i == _currentWeaponIndex;
            _weaponsList[i].gameObject.SetActive(_isCurrentWeapon);
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
        _weaponRotator?.LookAtMouse();
    }

    public void AddWeapon(WeaponSO _weaponSO)
    {
        var _w = Instantiate(_weaponSO.WeaponPrefab, transform.position, Quaternion.identity, transform);
        //_weaponsList.Add(_w);

        if (_weaponsList.Count >= _maxWeaponsCount)
        {
            // destroy a arma atual.
            // insert a nova no current index.
            Debug.Log($"Swap Weapons!");
        }
        else
        {
            _weaponsList.Add(_w);
            ChangeToNextWeapon();
            Debug.Log($"Add Weapon!");
        }
    }

    public bool HasWeapon(WeaponSO _weaponSO)
    {
        int _count = _weaponsList.Count;

        for (int i = 0; i < _count; i++)
        {
            if (_weaponsList[i].GetId() == _weaponSO.Id)
            {
                return true;
            }
        }

        return false;
    }

    [Button]
    private void SetWeaponReference()
    {
        _currentWeapon = GetComponentInChildren<WeaponBehaviour>();
    }
}
