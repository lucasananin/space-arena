using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFxHandler : MonoBehaviour
{
    [Title("// Hit Stop")]
    [SerializeField] float _hitStopTime = 0.2f;
    [SerializeField] float _hitStopScale = 0.1f;

    [Title("// Slow Motion")]
    [SerializeField] float _slowMoTime = 3f;
    [SerializeField] float _slowMoScale = 0.3f;
    [SerializeField, Range(0, 99)] int _odd = 10;

    private void OnEnable()
    {
        EnemyHealth.OnAnyAiDead += Play;
        PlayerHealth.OnPlayerDead += Play_HitAndSlow;
    }

    private void OnDisable()
    {
        EnemyHealth.OnAnyAiDead -= Play;
        PlayerHealth.OnPlayerDead -= Play_HitAndSlow;
    }

    private void Play(HealthBehaviour _health)
    {
        int _rand = Random.Range(0, 100);

        if (_rand < _odd)
        {
            PlaySlowMotion();
        }
        else
        {
            PlayHitStop();
        }
    }

    private void PlaySlowMotion()
    {
        StopAllCoroutines();
        StartCoroutine(Play_routine(_slowMoScale, _slowMoTime));
    }

    private void PlayHitStop()
    {
        StopAllCoroutines();
        StartCoroutine(Play_routine(_hitStopScale, _hitStopTime));
    }

    private IEnumerator Play_routine(float _scale, float _time)
    {
        Time.timeScale = _scale;
        yield return new WaitForSecondsRealtime(_time);
        Time.timeScale = 1;
    }

    private void Play_HitAndSlow(PlayerHealth _health)
    {
        StartCoroutine(Play_HitstopAndTheSlowMotion_routine());
    }

    private IEnumerator Play_HitstopAndTheSlowMotion_routine()
    {
        PlayHitStop();
        yield return new WaitForSecondsRealtime(_hitStopTime);
        PlaySlowMotion();
    }
}
