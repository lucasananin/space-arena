using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructFx : MonoBehaviour
{
    [SerializeField] Transform _renderTransform = null;
    [SerializeField] AudioCue _sfx = null;
    [Space]
    [SerializeField] Vector3 _strength = new(1f, 1f, 0f);
    [SerializeField, Range(0.1f, 9f)] float _strenghtMultiplier = 1f;
    [SerializeField, Range(0.1f, 9f)] float _duration = 1f;
    [SerializeField, Range(1, 99)] int _vibrato = 10;

    public void Play()
    {
        _sfx.PlayAudioCue();
        _renderTransform.DOComplete();
        _renderTransform.DOShakePosition(_duration, _strength * _strenghtMultiplier, _vibrato);
    }
}
