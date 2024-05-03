using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupView : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup = null;
    [SerializeField] float _fadeDuration = 0.3f;

    private void Awake()
    {
        InstantHide();
    }

    public void Show()
    {
        _canvasGroup.DOComplete();
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, _fadeDuration);
    }

    public void Hide()
    {
        _canvasGroup.DOComplete();
        _canvasGroup.alpha = 1;
        _canvasGroup.DOFade(0, _fadeDuration);
    }

    public void InstantHide()
    {
        _canvasGroup.DOComplete();
        _canvasGroup.alpha = 0;
    }

    public void ShowAndHide()
    {
        _canvasGroup.DOComplete();
        _canvasGroup.alpha = 1;
        _canvasGroup.DOFade(0, _fadeDuration).
            OnComplete(()=> 
            {
                Hide();
            });
    }
}
