using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUiHandler : MonoBehaviour
{
    [SerializeField] HealthBarUi _healthBarUi = null;

    private void Start()
    {
        var _playerHealth = FindAnyObjectByType<PlayerHealth>();
        _healthBarUi.Init(_playerHealth);
    }
}
