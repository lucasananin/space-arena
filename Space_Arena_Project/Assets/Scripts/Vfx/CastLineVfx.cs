using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CastLineVfx : MonoBehaviour
{
    [SerializeField] LineRenderer _line = null;
    //[SerializeField] float _fadeDuration = 0.3f;
    //[SerializeField] float _duration = 1f;
    [SerializeField] float _timeMultiplier = 1f;
    //[SerializeField] float _timeUntilDestroy = 1f;
    [SerializeField, ReadOnly] float _currentTime = 0f;

    private void Update()
    {
        var _previousWidth = _line.widthMultiplier;

        //var _multplier = 1 / (_duration * 2);
        //_currentTime += Time.deltaTime * _multplier;
        _currentTime += Time.deltaTime * _timeMultiplier;
        _line.widthMultiplier = Mathf.Lerp(_line.widthMultiplier, 0, _currentTime);
        //Debug.Log($"// {1 / _timeMultiplier}");

        if (_previousWidth != 0 && _line.widthMultiplier == 0)
        {
            Debug.Log($"// end: {Time.time}");
        }
    }

    public void Init(Vector3 _newPosition)
    {
        //var _c1 = new Color2(Color.white, Color.white);
        //var _c2 = new Color2(Color.clear, Color.clear);
        //_line.DOColor(_c1, _c2, _fadeDuration);

        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, _newPosition);

        StartCoroutine(DestroyRoutine());
        Debug.Log($"// start: {Time.time}");
    }

    private IEnumerator DestroyRoutine()
    {
        //yield return new WaitForSeconds(_fadeDuration * Random.Range(1f, 2f));
        //yield return new WaitForSeconds(_timeMultiplier * Random.Range(.5f, 2f));

        //yield return new WaitForSeconds(_timeUntilDestroy + Random.Range(1f, 2f));
        var _duration = 1 / _timeMultiplier;
        yield return new WaitForSeconds(_duration + Random.Range(1f, 2f));

        Destroy(gameObject);
    }
}
