using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedPanel : MonoBehaviour
{
    [SerializeField] CanvasGroupView _view = null;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += Show;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= Show;
    }

    private void Show(PlayerHealth _playerHealth)
    {
        _view.ShowAndHide();
    }
}
