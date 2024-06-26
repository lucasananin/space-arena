using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUiHandler : MonoBehaviour
{
    [SerializeField] HealthBarUi _healthBarUi = null;
    [SerializeField] AmmoUi _ammoUi = null;

    private void Start()
    {
        var _playerHealth = FindAnyObjectByType<PlayerHealth>();
        _healthBarUi.Init(_playerHealth);

        var _ammoHandler = FindAnyObjectByType<AmmoHandler>();
        _ammoUi.Init(_ammoHandler);
    }
}
