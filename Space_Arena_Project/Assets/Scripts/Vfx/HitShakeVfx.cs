using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[RequireComponent(typeof(HealthBehaviour))]
public class HitShakeVfx : MonoBehaviour
{
    [SerializeField] HealthBehaviour _healthBehaviour = null;
    [SerializeField] Transform _renderTransform = null;
    [Space]
    [SerializeField] Vector3 _strength = new Vector3(1f, 1f, 0f);
    [SerializeField, Range(0.1f, 9f)] float _strenghtMultiplier = 1f;
    [SerializeField, Range(0.1f, 1f)] float _duration = 1f;
    [Space]
    [SerializeField, ReadOnly] Vector3 _defaultPosition = default;

    private void Awake()
    {
        _defaultPosition = _renderTransform.localPosition;
    }

    private void OnEnable()
    {
        _healthBehaviour.onDamageTaken += Play;
    }

    private void OnDisable()
    {
        _healthBehaviour.onDamageTaken -= Play;
    }

    public void Play()
    {
        _renderTransform.DOComplete();
        _renderTransform.DOShakePosition(_duration, _strength * _strenghtMultiplier).
            OnComplete(() =>
            {
                _renderTransform.localPosition = _defaultPosition;
            });
    }
}
