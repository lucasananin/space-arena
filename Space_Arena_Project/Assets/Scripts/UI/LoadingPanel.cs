using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] CanvasGroupView _view = null;

    private void OnEnable()
    {
        EnemySpawner.OnStartWaveGroupChanged += Show;
        EnemySpawner.OnEndWaveGroupChanged += Hide;
    }

    private void OnDisable()
    {
        EnemySpawner.OnStartWaveGroupChanged -= Show;
        EnemySpawner.OnEndWaveGroupChanged -= Hide;
    }

    private void Show()
    {
        _view.Show();
    }

    private void Hide()
    {
        _view.Hide();
    }
}
