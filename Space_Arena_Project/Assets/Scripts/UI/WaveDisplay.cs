using TMPro;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] EnemySpawner _spawner = null;
    [SerializeField] TextMeshProUGUI _wave = null;
    [SerializeField] int _value = 0;
    [SerializeField] int _total = 0;

    private void Start()
    {
        _total = _spawner.GetTotalWaveCount();
        UpdateUI();
    }

    private void OnEnable()
    {
        EnemySpawner.OnStartWave += IncreaseValue;
    }

    private void OnDisable()
    {
        EnemySpawner.OnStartWave -= IncreaseValue;
    }

    private void IncreaseValue(WaveModel obj)
    {
        _value++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_value > 0)
        {
            _wave.text = $"{_value}/{_total}";
        }
        else
        {
            _wave.text = string.Empty;
        }
    }
}
