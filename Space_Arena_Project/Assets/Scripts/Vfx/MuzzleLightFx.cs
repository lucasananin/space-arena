using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MuzzleLightFx : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weapon = null;
    [SerializeField] Light2D _light2D = null;
    [Space]
    [SerializeField] float _minIntensity = 4f;
    [SerializeField] float _maxIntensity = 6f;
    [SerializeField] float _minRadiusMultiplier = 1f;
    [SerializeField] float _maxRadiusMultiplier = 2f;
    [Space]
    [SerializeField] float _duration = 0.08f;
    [Space]
    [SerializeField, ReadOnly] float _timer = 0f;
    [SerializeField, ReadOnly] float _initialIntensity = 0f;
    [SerializeField, ReadOnly] float _defaultInnerRadius = 0f;
    [SerializeField, ReadOnly] float _defaultOuterRadius = 0f;

    private void Awake()
    {
        _light2D.enabled = false;
        _defaultInnerRadius = _light2D.pointLightInnerRadius;
        _defaultOuterRadius = _light2D.pointLightOuterRadius;
    }

    private void OnEnable()
    {
        _weapon.onShoot += Init;
    }

    private void OnDisable()
    {
        _weapon.onShoot -= Init;
    }

    private void Update()
    {
        if (_timer > 1) return;

        _timer += Time.deltaTime * (1f / _duration);
        _light2D.intensity = Mathf.Lerp(_initialIntensity, 0f, _timer);
    }

    public void Init()
    {
        _timer = 0f;
        _light2D.enabled = true;
        _initialIntensity = Random.Range(_minIntensity, _maxIntensity);

        float _randomRadiusMultiplier = Random.Range(_minRadiusMultiplier, _maxRadiusMultiplier);
        _light2D.pointLightInnerRadius = _defaultInnerRadius * _randomRadiusMultiplier;
        _light2D.pointLightOuterRadius = _defaultOuterRadius * _randomRadiusMultiplier;
    }
}
