using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilFx : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weaponBehaviour = null;
    [SerializeField] Transform _targetTransform = null;
    [SerializeField] Vector3 _recoilOffset = new Vector3(-0.2f, 0f, 0f);
    [SerializeField] float _timeMultiplier = 5f;
    [SerializeField] float _duration = 0.1f;
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
        //_timer += _timer < 1 ? Time.deltaTime * _timeMultiplier : 0;
        _timer += _timer < 1 ? Time.deltaTime * (1f / _duration) : 0;
        _targetTransform.localPosition = Vector3.Lerp(_recoilPosition, _defaultPosition, _timer);
    }

    private void _weapon_onShoot()
    {
        _recoilPosition = _defaultPosition + _recoilOffset;
        _timer = 0f;
    }
}
