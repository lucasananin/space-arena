using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AiWeaponModel
{
    [SerializeField] List<WeaponBehaviour> _weapons = null;
    [SerializeField] Vector2 _shootRateRange = default;
    [SerializeField] bool _canRotateWhileShooting = true;
    [SerializeField] bool _canShootWhileMoving = true;
    [SerializeField] bool _onlyShootOnTargetAcquired = true;
    [SerializeField, Range(0.1f, 99f)] float _shootDistance = 12f;
    [SerializeField, ReadOnly] bool _isShooting = false;
    [SerializeField, ReadOnly] float _timeUntilShoot = 0f;
    [SerializeField, ReadOnly] float _shootTimer = 0f;
    [SerializeField, ReadOnly] List<WeaponRotator> _rotators = new();
    [SerializeField, ReadOnly] List<WeaponFlipper> _flippers = new();

    public bool IsShooting { get => _isShooting; set => _isShooting = value; }
    public bool CanShootWhileMoving { get => _canShootWhileMoving; private set => _canShootWhileMoving = value; }
    public bool OnlyShootOnTargetAcquired { get => _onlyShootOnTargetAcquired; private set => _onlyShootOnTargetAcquired = value; }
    public float ShootDistance { get => _shootDistance; private set => _shootDistance = value; }

    public void InitWeapons(EntityBehaviour _entitySource)
    {
        int _count = _weapons.Count;

        for (int i = 0; i < _count; i++)
        {
            _weapons[i].Init(_entitySource);
        }
    }

    public void ResetTime()
    {
        _timeUntilShoot = Random.Range(_shootRateRange.x, _shootRateRange.y);
        _shootTimer = 0;
    }

    public void IncreaseTime()
    {
        _shootTimer += Time.deltaTime;
    }

    public void RotateWeapons(Vector3 _position)
    {
        if (CanBlockRotation()) return;

        int _count = _weapons.Count;

        for (int i = 0; i < _count; i++)
        {
            _rotators[i].LookAtPosition(_position);
            _flippers[i].UpdateFlip();
        }
    }

    public void ResetWeaponRotations()
    {
        int _count = _weapons.Count;

        for (int i = 0; i < _count; i++)
        {
            _rotators[i].ResetRotation();
            _flippers[i].ResetFlip();
        }
    }

    public bool HasEnoughFireTime()
    {
        return _shootTimer > _timeUntilShoot;
    }

    public bool IsShootable()
    {
        return _shootRateRange.x + _shootRateRange.y > 0;
    }

    public bool CanBlockRotation()
    {
        return !_canRotateWhileShooting && _isShooting;
    }

    public WeaponBehaviour GetRandomWeapon()
    {
        int _randomIndex = Random.Range(0, _weapons.Count);
        return _weapons[_randomIndex];
    }

    //[Button]
    public void SetReferences()
    {
        if (_rotators is null) return;

        _rotators.Clear();
        _flippers.Clear();

        int _count = _weapons.Count;

        for (int i = 0; i < _count; i++)
        {
            _rotators.Add(_weapons[i].GetComponent<WeaponRotator>());
            _flippers.Add(_weapons[i].GetComponent<WeaponFlipper>());
        }
    }
}