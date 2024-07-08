using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFxHandler : MonoBehaviour
{
    [Title("// Hit Stop")]
    [SerializeField] float _time_hs = 0.1f;
    [SerializeField] float _scale_hs = 0.01f;

    [Title("// Slow Motion")]
    [SerializeField] float _time_sm = 0.1f;
    [SerializeField] float _scale_sm = 0.01f;
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
        StartCoroutine(Play_routine(_scale_sm, _time_sm));
    }

    private void PlayHitStop()
    {
        StartCoroutine(Play_routine(_scale_hs, _time_hs));
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
        yield return new WaitForSeconds(_time_hs);
        PlaySlowMotion();
    }
}
