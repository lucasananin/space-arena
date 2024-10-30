using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSfx : AudioCue
{
    [Title("// Health")]
    [SerializeField] HealthBehaviour _health = null;

    private void OnValidate()
    {
        _health = GetComponent<HealthBehaviour>();
    }

    private void OnEnable()
    {
        _health.OnDamageTaken += PlayAudioCue;
    }

    private void OnDisable()
    {
        _health.OnDamageTaken -= PlayAudioCue;
    }
}
