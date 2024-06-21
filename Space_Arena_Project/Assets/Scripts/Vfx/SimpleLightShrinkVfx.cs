using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SimpleLightShrinkVfx : MonoBehaviour
{
    [SerializeField] Light2D _light2D = null;
    [SerializeField] float _duration = 0.08f;
    [SerializeField, ReadOnly] float _timer = 0f;
    [SerializeField, ReadOnly] float _initialIntensity = 0f;

    private void Awake()
    {
        _initialIntensity = _light2D.intensity;
    }

    private void Update()
    {
        if (_timer > 1) return;

        _timer += Time.deltaTime * (1f / _duration);
        _light2D.intensity = Mathf.Lerp(_initialIntensity, 0f, _timer);
    }
}
