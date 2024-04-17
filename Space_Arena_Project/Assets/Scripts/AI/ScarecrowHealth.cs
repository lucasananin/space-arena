using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(HealthBehaviour))]
public class ScarecrowHealth : MonoBehaviour
{
    [SerializeField] HealthBehaviour _healthBehaviour = null;
    [SerializeField] Transform _renderTransform = null;
    [Space]
    [SerializeField] Vector3 _strength = new Vector3(1f, 1f, 0f);
    [SerializeField, Range(0.1f, 9f)] float _strenghtMultiplier = 1f;
    [SerializeField, Range(0.1f, 1f)] float _duration = 1f;

    private void OnEnable()
    {
        _healthBehaviour.onDamageTaken += Shake;
    }

    private void OnDisable()
    {
        _healthBehaviour.onDamageTaken -= Shake;
    }

    private void Shake()
    {
        _healthBehaviour.RestoreHealth();

        _renderTransform.DOComplete();
        _renderTransform.DOShakePosition(_duration, _strength * _strenghtMultiplier).
            OnComplete(() =>
            {
                _renderTransform.localPosition = Vector3.zero;
            });
    }
}
