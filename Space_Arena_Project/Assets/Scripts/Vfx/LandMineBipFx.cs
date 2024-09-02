using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineBipFx : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectile = null;
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] Sprite[] _sprites = null;
    [Space]
    [SerializeField, Range(0.1f, 0.9f)] float _normalizedMinValue = 0.5f;
    [SerializeField] float _defaultTimeMultiplier = 2f;
    [SerializeField] float _fastTimeMultiplier = 10f;
    [SerializeField, ReadOnly] float _timer = 0;

    private void LateUpdate()
    {
        var _normalizedExplodeTime = GetNormalizedExplodeTime();
        var _canIncreaseMultiplier = _normalizedExplodeTime >= _normalizedMinValue;
        var _multiplier = _canIncreaseMultiplier ? _fastTimeMultiplier : _defaultTimeMultiplier;

        _timer += Time.deltaTime * _multiplier;
        var _pingPong = Mathf.PingPong(_timer, _sprites.Length - 1);
        var _index = Mathf.RoundToInt(_pingPong);

        _renderer.sprite = _sprites[_index];
    }

    private float GetNormalizedExplodeTime()
    {
        return _projectile is null ? 0 : _projectile.GetExplodeTimeNormalized();
    }
}
