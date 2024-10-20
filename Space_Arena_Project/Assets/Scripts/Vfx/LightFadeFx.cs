using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFadeFx : MonoBehaviour
{
    [Title("// General")]
    [SerializeField] Light2D _light2D = null;
    [SerializeField] Vector2 _intensityRange = new(4f, 4f);
    [SerializeField] Vector2 _radiusRange = new(1f, 1f);
    [SerializeField] float _duration = 0.08f;
    [SerializeField] bool _playOnStart = false;
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

    private void Start()
    {
        if (_playOnStart)
        {
            Init();
        }
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
        _initialIntensity = Random.Range(_intensityRange.x, _intensityRange.y);

        float _randomRadiusMultiplier = Random.Range(_radiusRange.x, _radiusRange.y);
        _light2D.pointLightInnerRadius = _defaultInnerRadius * _randomRadiusMultiplier;
        _light2D.pointLightOuterRadius = _defaultOuterRadius * _randomRadiusMultiplier;
    }
}
