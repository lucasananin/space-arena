using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] CanvasGroupView _view = null;

    private void OnEnable()
    {
        SceneHandler.OnStartLoad += Show;
        SceneHandler.OnEndLoad += Hide;
        //EnemySpawner.OnStartWaveGroupChanged += Show;
        //EnemySpawner.OnEndWaveGroupChanged += Hide;
    }

    private void OnDisable()
    {
        SceneHandler.OnStartLoad -= Show;
        SceneHandler.OnEndLoad -= Hide;
        //EnemySpawner.OnStartWaveGroupChanged -= Show;
        //EnemySpawner.OnEndWaveGroupChanged -= Hide;
    }

    private void Show()
    {
        _view.Show();
    }

    private void Hide(WaveSO _waveSo)
    {
        Hide();
    }

    private void Hide()
    {
        _view.Hide();
    }
}
