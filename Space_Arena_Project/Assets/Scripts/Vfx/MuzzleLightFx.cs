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
    [SerializeField] float _minIntensity = 3f;
    [SerializeField] float _maxIntensity = 5f;
    [SerializeField] float _minRadiusMultiplier = 1f;
    [SerializeField] float _maxRadiusMultiplier = 2f;
    [Space]
    [SerializeField] float _timeEnabled = 0.05f;
    [Space]
    [SerializeField, ReadOnly] float _timer = 0f;
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
        _weapon.onShoot += Show;
    }

    private void OnDisable()
    {
        _weapon.onShoot -= Show;
    }

    private void Update()
    {
        if (!_light2D.enabled) return;

        _timer += Time.deltaTime;

        if (_timer >= _timeEnabled)
        {
            _light2D.enabled = false;
        }
    }

    public void Show()
    {
        _light2D.enabled = true;
        _light2D.intensity = Random.Range(_minIntensity, _maxIntensity);

        float _randomRadiusMultiplier = Random.Range(_minRadiusMultiplier, _maxRadiusMultiplier);
        _light2D.pointLightInnerRadius = _defaultInnerRadius * _randomRadiusMultiplier;
        _light2D.pointLightOuterRadius = _defaultOuterRadius * _randomRadiusMultiplier;

        _timer = 0f;
    }
}
