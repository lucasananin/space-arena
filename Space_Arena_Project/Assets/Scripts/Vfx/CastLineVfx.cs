using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CastLineVfx : MonoBehaviour
{
    [SerializeField] LineRenderer _line = null;
    [SerializeField] float _timeMultiplier = 1f;
    [SerializeField, ReadOnly] float _currentTime = 0f;

    private void Update()
    {
        _currentTime += Time.deltaTime * _timeMultiplier;
        _line.widthMultiplier = Mathf.Lerp(_line.widthMultiplier, 0, _currentTime);
    }

    public void Init(Vector3 _newPosition)
    {
        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, _newPosition);

        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine()
    {
        float _duration = 1f / _timeMultiplier;
        yield return new WaitForSeconds(_duration + Random.Range(1f, 2f));

        Destroy(gameObject);
    }
}
