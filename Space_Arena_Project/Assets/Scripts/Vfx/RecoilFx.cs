using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecoilFx : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weaponBehaviour = null;
    [SerializeField] Transform _targetTransform = null;
    [SerializeField] Animator _animator = null;
    [SerializeField] Vector3 _offsetPosition = default;
    [SerializeField] float _xOffset = 0.2f;
    [SerializeField] float _recoilMultiplier = 1f;
    [SerializeField] float _timeMultiplier = 1f;
    [Space]
    [SerializeField, ReadOnly] Vector3 _defaultPosition = default;
    [SerializeField, ReadOnly] Vector3 _recoilPosition = default;
    [SerializeField, ReadOnly] float _timer = 0f;

    private void Awake()
    {
        _defaultPosition = _targetTransform.localPosition;
        _timer = 1f;
    }

    private void OnEnable()
    {
        _weaponBehaviour.onShoot += _weapon_onShoot;
    }

    private void OnDisable()
    {
        _weaponBehaviour.onShoot -= _weapon_onShoot;
    }

    private void Update()
    {
        _timer += _timer < 1 ? Time.deltaTime * _timeMultiplier : 0;
        _targetTransform.localPosition = Vector3.Lerp(_recoilPosition, _defaultPosition, _timer);
    }

    private void _weapon_onShoot()
    {
        //Vector3 _weaponBackDirection = -_weaponBehaviour.transform.right.normalized;
        Vector3 _weaponBackDirection = -_targetTransform.right;
        Vector3 _localBackPosition = _targetTransform.InverseTransformPoint(_weaponBackDirection);

        _recoilPosition = (_localBackPosition * _recoilMultiplier) + _defaultPosition;
        _recoilPosition.y = _defaultPosition.y;

        //_targetTransform.localPosition = _recoilPosition;
        //_targetTransform.DOComplete();
        //_targetTransform.DOLocalMove(_defaultPosition, _recoilMultiplier);

        //_animator.SetTrigger("Shoot");

        //_recoilPosition = _offsetPosition;
        _recoilPosition = _defaultPosition;
        _recoilPosition.x -= _recoilMultiplier;

        _timer = 0f;
    }
}
