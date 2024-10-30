using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSx : AudioCue
{
    [Title("// Health")]
    [SerializeField] HealthBehaviour _health = null;

    private void OnValidate()
    {
        _health = GetComponent<HealthBehaviour>();
    }

    private void OnEnable()
    {
        _health.OnDead += PlayAudioCue;
    }

    private void OnDisable()
    {
        _health.OnDead -= PlayAudioCue;
    }
}
