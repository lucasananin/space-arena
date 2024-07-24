using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarLineSpawner : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectile = null;
    [SerializeField] StraightLineVfx _lineVfx = null;

    private void OnEnable()
    {
        _projectile.OnDestroy_TimerEnd += _projectile_OnDestroy_TimerEnd;
    }

    private void OnDisable()
    {
        _projectile.OnDestroy_TimerEnd -= _projectile_OnDestroy_TimerEnd;
    }

    private void _projectile_OnDestroy_TimerEnd()
    {
        var _initialPosition = Vector2.up * 20f + Vector2.left * -2;
        var _instance = Instantiate(_lineVfx, _initialPosition, transform.rotation);

        var _finalPosition = transform.position;
        _instance.Init(_finalPosition);
    }
}
