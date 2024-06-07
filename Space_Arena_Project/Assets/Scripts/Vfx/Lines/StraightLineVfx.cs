using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class StraightLineVfx : LaserLineVfx
{
    //[SerializeField] LineRenderer[] _lineRenderers= null;
    //[SerializeField] float _timeMultiplier = 1f;
    //[SerializeField, ReadOnly] List<float> _defaultWidths = null;
    //[SerializeField, ReadOnly] float _currentTime = 0f;

    //private void Awake()
    //{
    //    int _count = _lineRenderers.Length;

    //    for (int i = 0; i < _count; i++)
    //    {
    //        _defaultWidths.Add(_lineRenderers[i].widthMultiplier);
    //    }
    //}

    //private void Update()
    //{
    //    _currentTime += Time.deltaTime * _timeMultiplier;

    //    int _count = _lineRenderers.Length;

    //    for (int i = 0; i < _count; i++)
    //    {
    //        _lineRenderers[i].widthMultiplier = Mathf.Lerp(_defaultWidths[i], 0f, _currentTime);
    //    }
    //}

    public void Init(Vector3 _newPosition)
    {
        int _count = _lineRenderers.Length;

        for (int i = 0; i < _count; i++)
        {
            _lineRenderers[i].SetPosition(0, transform.position);
            _lineRenderers[i].SetPosition(1, _newPosition);
        }

        ResetShrinkTimer();
        StartDestroyRoutine();
    }

    //private IEnumerator DestroyRoutine()
    //{
    //    float _duration = 1f / _timeMultiplier;
    //    yield return new WaitForSeconds(_duration + Random.Range(1f, 2f));

    //    Destroy(gameObject);
    //}
}
