using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingShotVfx : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weapon = null;
    [SerializeField] ParticleSystem _ps = null;
    [SerializeField] Transform _circle = null;
    [SerializeField] Vector3 _maxScale = Vector3.one;

    private void Awake()
    {
        ResetValues();
    }

    private void OnEnable()
    {
        _weapon.onPullTrigger += Play;
        _weapon.onReleaseTrigger += Stop;
        _weapon.onShoot += Stop;
    }

    private void OnDisable()
    {
        _weapon.onPullTrigger -= Play;
        _weapon.onReleaseTrigger -= Stop;
        _weapon.onShoot -= Stop;
    }

    private void Play()
    {
        _ps.Play();

        ResetValues();
        var _duration = _weapon.ChargingTime;
        _circle.DOScale(_maxScale, _duration);
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
