using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwingFx : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weapon = null;
    [SerializeField] Transform _transform = null;
    [SerializeField] Ease _ease = default;
    [SerializeField] float _degrees = 60f;
    [SerializeField] float _duration = 0.1f;
    [SerializeField, ReadOnly] bool _isUp = true;
    [SerializeField, ReadOnly] Vector3 _defaultEuler = default;
    [SerializeField, ReadOnly] Vector3 _targetEuler = default;

    private void Awake()
    {
        _defaultEuler = _transform.rotation.eulerAngles;
        _targetEuler = _defaultEuler + Vector3.forward * _degrees;
    }

    private void OnEnable()
    {
        _weapon.onShoot += _weapon_onShoot;
    }

    private void OnDisable()
    {
        _weapon.onShoot -= _weapon_onShoot;
    }

    private void _weapon_onShoot()
    {
        _isUp = !_isUp;

        //var _multiplier = _isUp ? _degrees : -_degrees;
        //var _endValue = new Vector3(0, 0, _transform.localRotation.eulerAngles.z + _multiplier);

        var _xMultiplier = _isUp ? 1 : -1f;
        _transform.localScale = new Vector3(_xMultiplier, 1, 1);
        //_transform.localScale += Vector3.right * _xMultiplier;

        var _endValue = _isUp ? _defaultEuler : _targetEuler;
        _transform.DOLocalRotate(_endValue, _duration).
            SetEase(_ease);
    }
}
