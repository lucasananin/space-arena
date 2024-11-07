using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgamePanel : MonoBehaviour
{
    [SerializeField] CanvasGroupView _victoryPanel = null;
    [SerializeField] CanvasGroupView _defeatPanel = null;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDead += ShowDefeatPanel;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDead -= ShowDefeatPanel;
    }

    private void ShowDefeatPanel(PlayerHealth obj)
    {
        _defeatPanel.Show();
    }

    public void ShowVictoryPanel()
    {
        _victoryPanel.Show();
    }
}
