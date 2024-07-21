using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillPanel : MonoBehaviour
{
    [SerializeField] CanvasGroupView _view = null;

    private void OnEnable()
    {
        EnemyHealth.OnAnyAiDead += Show;
    }

    private void OnDisable()
    {
        EnemyHealth.OnAnyAiDead += Show;
    }

    private void Show(HealthBehaviour _healthBehaviour)
    {
        _view.ShowAndHide();
    }
}
