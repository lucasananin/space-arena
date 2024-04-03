using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WeaponLightTest : MonoBehaviour
{
    [SerializeField] Light2D _light2D = null;
    [SerializeField] WeaponBehaviour _weapon = null;
    [SerializeField] float _minIntensity = 3f;
    [SerializeField] float _maxIntensity = 5f;
    [SerializeField] float _minRadiusMultiplier = 1f;
    [SerializeField] float _maxRadiusMultiplier = 2f;
    [Space]
    [SerializeField] float _timeEnabled = 0.05f;
    [SerializeField, ReadOnly] float _timeUntilDisable = 0;

    private float _defaultInnerRadius = 0;
    private float _defaultOuterRadius = 0;

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
        _timeUntilDisable -= _timeUntilDisable > 0 ? Time.deltaTime : 0;

        if (_timeUntilDisable <= 0)
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

        _timeUntilDisable = _timeEnabled;
    }
}
