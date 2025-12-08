using System.Collections;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] AudioCue _defaultMusic = null;
    [SerializeField] AudioCue _bossMusic = null;

    private void OnEnable()
    {
        EnemySpawner.OnEndWave += StopDefaultMusic;
        EnemySpawner.OnEndWave += StopBossMusic;
        EnemySpawner.OnStartWave += PlayBossMusic;
    }

    private void OnDisable()
    {
        EnemySpawner.OnEndWave -= StopDefaultMusic;
        EnemySpawner.OnEndWave -= StopBossMusic;
        EnemySpawner.OnStartWave -= PlayBossMusic;
    }

    private void StopDefaultMusic(WaveModel _wave)
    {
        if (!_defaultMusic.gameObject.activeSelf) return;
        StartCoroutine(StopDefaultMusic_Routine());
    }

    private IEnumerator StopDefaultMusic_Routine()
    {
        yield return null;

        var _enemySpawner = FindFirstObjectByType<EnemySpawner>();
        if (_enemySpawner.IsBossWaveGroup())
        {
            _defaultMusic.gameObject.SetActive(false);
        }
    }

    private void PlayBossMusic(WaveModel _wave)
    {
        if (!_defaultMusic.gameObject.activeSelf)
        {
            //_bossMusic.PlayAudioCue();
            _bossMusic.gameObject.SetActive(true);
        }
    }

    private void StopBossMusic(WaveModel _wave)
    {
        if (_bossMusic.gameObject.activeSelf)
            _bossMusic.gameObject.SetActive(false);
    }
}
