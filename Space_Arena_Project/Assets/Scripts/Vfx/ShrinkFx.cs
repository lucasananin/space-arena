using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkFx : MonoBehaviour
{
    [SerializeField] Transform[] _transforms = null;
    [SerializeField] Vector3 _targetScale = Vector3.zero;
    [SerializeField] float _shrinkDuration = 0.3f;
    [SerializeField, ReadOnly] List<Vector3> _defaultScales = null;
    [SerializeField, ReadOnly] float _shrinkTimer = 0f;

    private void Awake()
    {
        SetDefaults();
    }

    private void Update()
    {
        ShrinkRenderers();
    }

    private void ShrinkRenderers()
    {
        if (_shrinkTimer > 1) return;

        _shrinkTimer += Time.deltaTime * (1f / _shrinkDuration);
        int _count = _transforms.Length;

        for (int i = 0; i < _count; i++)
        {
            _transforms[i].localScale = Vector3.Lerp(_defaultScales[i], _targetScale, _shrinkTimer);
        }
    }

    private void SetDefaults()
    {
        int _count = _transforms.Length;

        for (int i = 0; i < _count; i++)
        {
            _defaultScales.Add(_transforms[i].localScale);
        }
    }
}
