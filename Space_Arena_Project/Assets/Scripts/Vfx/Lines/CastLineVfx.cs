using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CastLineVfx : MonoBehaviour
{
    [SerializeField] protected LineRenderer[] _lineRenderers = null;
    [SerializeField] protected float _shrinkDuration = 0.3f;
    [SerializeField, ReadOnly] protected List<float> _defaultWidths = null;
    [SerializeField, ReadOnly] protected float _shrinkTimer = 0f;

    protected virtual void Awake()
    {
        SetDefaults();
    }

    protected virtual void Update()
    {
        ShrinkRenderers();
    }

    protected void ShrinkRenderers()
    {
        if (_shrinkTimer > 1) return;

        _shrinkTimer += Time.deltaTime * (1f / _shrinkDuration);
        int _count = _lineRenderers.Length;

        for (int i = 0; i < _count; i++)
        {
            _lineRenderers[i].widthMultiplier = Mathf.Lerp(_defaultWidths[i], 0f, _shrinkTimer);
        }
    }

    protected void SetDefaults()
    {
        int _count = _lineRenderers.Length;

        for (int i = 0; i < _count; i++)
        {
            _defaultWidths.Add(_lineRenderers[i].widthMultiplier);
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
        float _waitTime = _shrinkDuration + Random.Range(1f, 2f);
        yield return new WaitForSeconds(_waitTime);
        Destroy(gameObject);
    }
}
