using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaserLineVfx : MonoBehaviour
{
    [SerializeField] protected LineRenderer[] _lineRenderers = null;
    [SerializeField] protected float _shrinkTimeMultiplier = 1f;
    [SerializeField, ReadOnly] protected List<float> _defaultWidths = null;
    [SerializeField, ReadOnly] protected float _shrinkTimer = 0f;

    protected virtual void Awake()
    {
        int _count = _lineRenderers.Length;

        for (int i = 0; i < _count; i++)
        {
            _defaultWidths.Add(_lineRenderers[i].widthMultiplier);
        }
    }

    protected virtual void Update()
    {
        ShrinkRenderers();
    }

    protected void ShrinkRenderers()
    {
        if (_shrinkTimer > 1) return;

        _shrinkTimer += Time.deltaTime * _shrinkTimeMultiplier;

        int _count = _lineRenderers.Length;

        for (int i = 0; i < _count; i++)
        {
            _lineRenderers[i].widthMultiplier = Mathf.Lerp(_defaultWidths[i], 0f, _shrinkTimer);
        }
    }

    protected void ResetShrinkTimer()
    {
        _shrinkTimer = 0f;
    }

    protected void StartDestroyRoutine()
    {
        StartCoroutine(Destroy_routine());
    }

    protected IEnumerator Destroy_routine()
    {
        float _animDuration = 1f / _shrinkTimeMultiplier;
        float _waitTime = _animDuration + Random.Range(1f, 2f);
        yield return new WaitForSeconds(_waitTime);
        Destroy(gameObject);
    }

    //public abstract void Init(RaycastHit2D raycastHit2D);
}
