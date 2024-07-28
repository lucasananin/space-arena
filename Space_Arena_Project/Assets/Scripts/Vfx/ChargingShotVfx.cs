using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingShotVfx : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weapon = null;
    [SerializeField] ParticleSystem _ps = null;
    [SerializeField] Transform _circle = null;
    [SerializeField] Ease _ease = default;
    [SerializeField, Range(0.1f, 2f)] float _scaleMultiplier = 0.5f;

    private void Awake()
    {
        ResetValues();
    }

    private void OnEnable()
    {
        _weapon.OnPullTrigger += Play;
        _weapon.OnReleaseTrigger += Stop;
        _weapon.OnShoot += Stop;
    }

    private void OnDisable()
    {
        _weapon.OnPullTrigger -= Play;
        _weapon.OnReleaseTrigger -= Stop;
        _weapon.OnShoot -= Stop;
    }

    private void Play()
    {
        _ps.Play();

        ResetValues();
        var _duration = _weapon.ChargingTime;
        _circle.DOScale(_scaleMultiplier, _duration).SetEase(_ease);
    }

    private void Stop()
    {
        _ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ResetValues();
    }

    private void ResetValues()
    {
        _circle.DOKill();
        _circle.localScale = Vector3.zero;
    }
}
