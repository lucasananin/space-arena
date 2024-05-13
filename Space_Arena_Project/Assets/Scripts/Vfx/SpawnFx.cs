using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFx : MonoBehaviour
{
    [SerializeField] Transform _transform = null;
    [SerializeField] float _duration = 1f;
    [SerializeField, ReadOnly] bool _isAnimating = false;

    public bool IsAnimating { get => _isAnimating; private set => _isAnimating = value; }

    public void Play()
    {
        _isAnimating = true;
        _transform.localScale = Vector3.zero;

        _transform.DOScale(Vector3.one, _duration).
            OnComplete(() =>
            {
                _isAnimating = false;
            });
    }
}
