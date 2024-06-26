using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] EntityBehaviour _entitySource = null;
    [SerializeField] SideFlipper _entityFlipper = null;
    [SerializeField] AmmoHandler _ammoHandler = null;
    [SerializeField] EntityHolster _holster = null;
    [SerializeField, Range(1, 9)] int _maxWeaponsCount = 2;
    [SerializeField, Range(0f, 1f)] float _swapInputDelay = 0.3f;
    [SerializeField] List<WeaponBehaviour> _weaponsList = null;

    [Title("// Debug")]
    [SerializeField, ReadOnly] WeaponBehaviour _currentWeapon = null;
    [SerializeField, ReadOnly] WeaponBehaviour _lastWeapon = null;
    [SerializeField, ReadOnly] WeaponRotator _weaponRotator = null;
    [SerializeField, ReadOnly] WeaponFlipper _weaponFlipper = null;
    [SerializeField, ReadOnly] int _currentWeaponIndex = 0;
    [SerializeField, ReadOnly] int _lastWeaponIndex = 0;
    [SerializeField, ReadOnly] bool _isWaitingSwapDelay = false;
    [SerializeField, ReadOnly] bool _canRotateWeapon = false;

    public bool CanRotateWeapon { get => _canRotateWeapon; set => _canRotateWeapon = value; }

    private void Start()
    {
        SetCurrentWeapon();
    }

    private void OnEnable()
    {
        InputHandler.onLeftMouseButtonDown += PullTrigger;
        InputHandler.onLeftMouseButtonUp += ReleaseTrigger;
        InputHandler.onMouseScrollSwipe += SwapThroughInput;
        _entityFlipper.onFlip += RotateCurrentWeapon;
        //InputHandler.onMouseScrollUp += SwapToNextWeapon;
        //InputHandler.onMouseScrollDown += SwapToPreviousWeapon;
    }

    private void OnDisable()
    {
        InputHandler.onLeftMouseButtonDown -= PullTrigger;
        InputHandler.onLeftMouseButtonUp -= ReleaseTrigger;
        InputHandler.onMouseScrollSwipe -= SwapThroughInput;
        _entityFlipper.onFlip -= RotateCurrentWeapon;
        //InputHandler.onMouseScrollUp -= SwapToNextWeapon;
        //InputHandler.onMouseScrollDown -= SwapToPreviousWeapon;
    }

    private void FixedUpdate()
    {
        if (_canRotateWeapon)
        {
            RotateCurrentWeapon();
        }
    }

    private void PullTrigger()
    {
        _currentWeapon?.PullTrigger();
    }

    private void ReleaseTrigger()
    {
        _currentWeapon?.ReleaseTrigger();
    }

    private void SwapThroughInput(float _y)
    {
        if (_isWaitingSwapDelay) return;

        if (_y > 0)
            SwapToNextWeapon();
        else if (_y < 0)
            SwapToPreviousWeapon();

        StartCoroutine(DisableWeaponSwap_routine());
    }

    private void SwapToNextWeapon()
    {
        IncreaseWeaponIndex(1);
    }

    private void SwapToPreviousWeapon()
    {
        IncreaseWeaponIndex(-1);
    }

    private void IncreaseWeaponIndex(int _value)
    {
        _lastWeaponIndex = HasMoreThanOneWeapon() ? _currentWeaponIndex : -1;
        _currentWeaponIndex = _weaponsList.IndexOf(_currentWeapon);
        _currentWeaponIndex += _value;

        if (_currentWeaponIndex >= _weaponsList.Count)
            _currentWeaponIndex = 0;

        if (_currentWeaponIndex < 0)
            _currentWeaponIndex = _weaponsList.Count - 1;

        SetCurrentWeapon();
    }

    private void SetCurrentWeapon()
    {
        if (_weaponsList.Count <= 0) return;

        _currentWeapon = _weaponsList[_currentWeaponIndex];
        _lastWeapon = HasMoreThanOneWeapon() ? _weaponsList[_lastWeaponIndex] : null;
        _currentWeapon.Init(_entitySource, _ammoHandler);

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        int _count = _weaponsList.Count;

        for (int i = 0; i < _count; i++)
        {
            bool _isCurrentWeapon = i == _currentWeaponIndex;
            _weaponsList[i].gameObject.SetActive(_isCurrentWeapon);
        }

        _weaponRotator = _currentWeapon.GetComponent<WeaponRotator>();
        _weaponFlipper = _currentWeapon.GetComponent<WeaponFlipper>();
        RotateCurrentWeapon();
        UpdateHolster();
    }

    private void UpdateHolster()
    {
        bool _canUpdate = _lastWeapon is not null && HasMoreThanOneWeapon();
        var _so = _canUpdate ? _lastWeapon.WeaponSO : null;
        _holster.Init(_so);
    }

    public void RotateCurrentWeapon()
    {
        _weaponRotator?.LookAtMouse();
        _weaponFlipper?.UpdateFlip();
    }

    public void AddWeapon(WeaponSO _weaponSO, out WeaponSO _droppedWeaponSO)
    {
        var _newWeapon = Instantiate(_weaponSO.WeaponPrefab, transform.position, Quaternion.identity, transform);

        if (_weaponsList.Count >= _maxWeaponsCount)
        {
            _weaponsList[_currentWeaponIndex] = _newWeapon;
            _droppedWeaponSO = _currentWeapon.WeaponSO;
            Destroy(_currentWeapon.gameObject);
            SetCurrentWeapon();
        }
        else
        {
            _weaponsList.Add(_newWeapon);
            _droppedWeaponSO = null;
            SwapToNextWeapon();
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

    private bool HasMoreThanOneWeapon()
    {
        return _weaponsList.Count > 1;
    }

    public WeaponBehaviour GetFirstWeapon()
    {
        if (_currentWeapon is null) return null;
        return _weaponsList[0];
    }

    public WeaponBehaviour GetSecondWeapon()
    {
        if (!HasMoreThanOneWeapon()) return null;
        return _weaponsList[1];
    }

    private IEnumerator DisableWeaponSwap_routine()
    {
        _isWaitingSwapDelay = true;
        yield return new WaitForSeconds(_swapInputDelay);
        _isWaitingSwapDelay = false;
    }

    //[Button]
    //private void SetWeaponReference()
    //{
    //    _currentWeapon = GetComponentInChildren<WeaponBehaviour>();
    //}
}
