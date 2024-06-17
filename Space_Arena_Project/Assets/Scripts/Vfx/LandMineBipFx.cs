using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineBipFx : MonoBehaviour
{
    [SerializeField] Sprite[] _sprites = null;
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] ProjectileBehaviour _projectile = null;
    [SerializeField, Range(0.1f, 0.9f)] float _normalizedMinValue = 0.5f;
    [SerializeField] float _defaultTimeMultiplier = 1f;
    [SerializeField] float _fastTimeMultiplier = 2f;
    [SerializeField, ReadOnly] float _t = 0;

    private void LateUpdate()
    {
        var _normalizedExplodeTime = _projectile.GetExplodeTimeNormalized();
        var _canIncreaseMultiplier = _normalizedExplodeTime >= _normalizedMinValue;
        var _multiplier = _canIncreaseMultiplier ? _fastTimeMultiplier : _defaultTimeMultiplier;

        _t += Time.deltaTime * _multiplier;
        var _pingPong = Mathf.PingPong(_t, _sprites.Length - 1);
        var _index = Mathf.RoundToInt(_pingPong);

        _renderer.sprite = _sprites[_index];
    }
}
