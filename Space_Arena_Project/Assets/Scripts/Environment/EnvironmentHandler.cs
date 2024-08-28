using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour
{
    [SerializeField] GraphUpdateTest _graphUpdater = null;
    [SerializeField] GameObject _defaultEnvironment = null;
    [SerializeField, ReadOnly] GameObject _currentEnvironment = null;

    private void Awake()
    {
        _currentEnvironment = _defaultEnvironment;
    }

    private void OnEnable()
    {
        EnemySpawner.OnEndWaveGroupChanged += ChangeEnvironment;
    }

    private void OnDisable()
    {
        EnemySpawner.OnEndWaveGroupChanged -= ChangeEnvironment;
    }

    private void ChangeEnvironment(WaveSO _waveSo)
    {
        _currentEnvironment.SetActive(false);

        var _instance = Instantiate(_waveSo.Environment, transform.position, Quaternion.identity);
        _currentEnvironment = _instance;
        _graphUpdater.UpdateGraph();
    }
}
