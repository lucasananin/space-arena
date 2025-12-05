using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] float _value = 0;
    [SerializeField] float _recoveryRate = 1f;
    [SerializeField, ReadOnly] AudioManager _manager = null;

    private void Awake()
    {
        _manager = FindFirstObjectByType<AudioManager>();
    }

    private void OnEnable()
    {
        EnemySpawner.OnEndWave += DecreaseVolume;
        EnemySpawner.OnStartWave += IncreaseVolume;
    }

    private void OnDisable()
    {
        EnemySpawner.OnEndWave -= DecreaseVolume;
        EnemySpawner.OnStartWave -= IncreaseVolume;
    }

    private void DecreaseVolume(WaveModel model)
    {
        StopAllCoroutines();
        StartCoroutine(DecreaseRoutine(_value * 0.5f));
    }

    private void IncreaseVolume(WaveModel obj)
    {
        StopAllCoroutines();
        StartCoroutine(DecreaseRoutine(_value));
    }

    private IEnumerator DecreaseRoutine(float _target)
    {
        float _t = 0f;

        while (_t < 5f)
        {
            var _v = Mathf.MoveTowards(_manager.MusicVolume, _target, _recoveryRate * Time.deltaTime);
            _manager.ChangeMusicVolume(_v);

            yield return null;
            _t += Time.deltaTime;
        }
    }
}
