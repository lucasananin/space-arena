using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChromaticPulseFx : MonoBehaviour
{
    [SerializeField] Volume _volume = null;
    [SerializeField] float _duration = 1f;
    [SerializeField, ReadOnly] ChromaticAberration _cAbe = null;
    [SerializeField, ReadOnly] float _time = 0f;

    private void Awake()
    {
        InitReferences();
    }

    private void OnEnable()
    {
        EnemyHealth.OnAnyAiDead += ResetTime;
    }

    private void OnDisable()
    {
        EnemyHealth.OnAnyAiDead -= ResetTime;
    }

    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        if (_time > 1f) return;
        _time += Time.deltaTime * (1f / _duration);
        _cAbe.intensity.Override(Mathf.Lerp(1f, 0f, _time));
    }

    private void ResetTime(HealthBehaviour _health)
    {
        _time = 0f;
    }

    private void InitReferences()
    {
        VolumeProfile _volumeProfile = _volume.profile;

        if (!_volumeProfile)
        {
            throw new System.NullReferenceException(nameof(VolumeProfile));
        }

        if (_volumeProfile.TryGet(out ChromaticAberration _cAbe))
        {
            this._cAbe = _cAbe;
        }
        else
        {
            throw new System.NullReferenceException(nameof(_cAbe));
        }
    }

    [Button]
    private void Play()
    {
        ResetTime(null);
    }
}
