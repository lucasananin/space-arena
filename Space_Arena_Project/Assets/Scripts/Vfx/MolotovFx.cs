using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovFx : MonoBehaviour
{
    [SerializeField] DamageArea _damageArea = null;
    [SerializeField] ParticleSystem _ps = null;
    [SerializeField] Transform _transform = null;
    [SerializeField] float _duration = 1f;
    [SerializeField, ReadOnly] Vector3 _defaultScale = default;

    private void Awake()
    {
        _defaultScale = _transform.localScale;
    }

    private void Start()
    {
        Play();
    }

    private void OnEnable()
    {
        _damageArea.OnDestroyStart += StopPS;
    }

    private void OnDisable()
    {
        _damageArea.OnDestroyStart -= StopPS;
    }

    public void Play()
    {
        _transform.localScale = Vector3.zero;
        _transform.DOScale(_defaultScale, _duration);
    }

    public void StopPS()
    {
        _ps.Stop();
        _transform.DOScale(0, _duration);
    }
}
