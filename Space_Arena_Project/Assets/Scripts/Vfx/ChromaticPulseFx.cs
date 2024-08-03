using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChromaticPulseFx : MonoBehaviour
{
    [SerializeField] Volume _volume = null;
    [SerializeField] Vector2 _range = Vector2.right;
    [SerializeField] float _duration = 1f;
    [SerializeField, ReadOnly] ChromaticAberration _component = null;
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
        _component.intensity.Override(Mathf.Lerp(_range.x, _range.y, _time));
    }

    private void ResetTime(HealthBehaviour _health)
    {
        _time = 0f;
    }

    private void InitReferences()
    {
        var _profile = _volume.profile;

        if (!_profile)
        {
            throw new System.NullReferenceException(nameof(VolumeProfile));
        }

        if (_profile.TryGet(out ChromaticAberration _cAbe))
        {
            _component = _cAbe;
        }
        else
        {
            throw new System.NullReferenceException(nameof(_cAbe));
        }
    }
}
