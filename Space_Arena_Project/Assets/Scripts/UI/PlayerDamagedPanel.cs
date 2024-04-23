using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedPanel : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup = null;
    [SerializeField] float _fadeDuration = 0.3f;

    private void Awake()
    {
        Hide();
    }

    private void OnEnable()
    {
        PlayerHealth.onPlayerDamaged += Show;
    }

    private void OnDisable()
    {
        PlayerHealth.onPlayerDamaged -= Show;
    }

    private void Show(PlayerHealth _playerHealth)
    {
        _canvasGroup.DOComplete();
        _canvasGroup.alpha = 1;
        _canvasGroup.DOFade(0f, _fadeDuration);
    }

    private void Hide()
    {
        _canvasGroup.alpha = 0;
    }
}
