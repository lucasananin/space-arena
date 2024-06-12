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
    [SerializeField] Vector2 _minMaxSlerpTime = new Vector2(0.2f, 0.8f);
    [SerializeField] float _degrees = 60f;
    [SerializeField] float _duration = 0.1f;
    [SerializeField, ReadOnly] bool _isUp = true;
    [SerializeField, ReadOnly] Vector3 _defaultEuler = default;
    [SerializeField, ReadOnly] Vector3 _targetEuler = default;

    private void Awake()
    {
        SetEulers();
    }

    private void OnEnable()
    {
        _weapon.onShoot += Play;
    }

    private void OnDisable()
    {
        _weapon.onShoot -= Play;
    }

    private void Play()
    {
        _isUp = !_isUp;
        Flip();
        Rotate();
    }

    private void Rotate()
    {
        //var _multiplier = _isUp ? _degrees : -_degrees;
        //var _endValue = new Vector3(0, 0, _transform.localRotation.eulerAngles.z + _multiplier);
        var _t = _isUp ? _minMaxSlerpTime.x : _minMaxSlerpTime.y;
        var _rotation = Quaternion.Slerp(Quaternion.Euler(_defaultEuler), Quaternion.Euler(_targetEuler), _t);
        _transform.localRotation = _rotation;

        var _endValue = _isUp ? _defaultEuler : _targetEuler;
        _transform.DOLocalRotate(_endValue, _duration).
            SetEase(_ease);
    }

    private void Flip()
    {
        var _xMultiplier = _isUp ? 1 : -1f;
        _transform.localScale = new Vector3(_xMultiplier, 1, 1);
        //_transform.localScale += Vector3.right * _xMultiplier;
    }

    private void SetEulers()
    {
        _defaultEuler = _transform.localRotation.eulerAngles;
        _targetEuler = _defaultEuler + Vector3.forward * _degrees;
    }
}
