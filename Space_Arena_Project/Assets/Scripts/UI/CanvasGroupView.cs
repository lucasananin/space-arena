using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupView : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup = null;
    [SerializeField] float _fadeDuration = 0.3f;

    private bool _defaultInteractable = false;
    private bool _defaultBlockRaycasts = false;

    private void Awake()
    {
        _defaultInteractable = _canvasGroup.interactable;
        _defaultBlockRaycasts = _canvasGroup.blocksRaycasts;
        InstantHide();
    }

    public void Show()
    {
        _canvasGroup.DOComplete();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = _defaultInteractable;
        _canvasGroup.blocksRaycasts = _defaultBlockRaycasts;
        _canvasGroup.DOFade(1, _fadeDuration);
    }

    public void Hide()
    {
        _canvasGroup.DOComplete();
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0, _fadeDuration);
    }

    public void InstantHide()
    {
        _canvasGroup.DOComplete();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void ShowAndHide()
    {
        _canvasGroup.DOComplete();
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0, _fadeDuration);
        //_canvasGroup.DOFade(0, _fadeDuration).
        //    OnComplete(()=> 
        //    {
        //        Hide();
        //    });
    }

    public bool IsVisible()
    {
        return _canvasGroup.alpha > 0;
    }
}
