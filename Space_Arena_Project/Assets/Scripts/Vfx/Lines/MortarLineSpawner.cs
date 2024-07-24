using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarLineSpawner : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectile = null;
    [SerializeField] StraightLineVfx _lineVfx = null;
    [SerializeField] Vector3 _offset = default;

    private void OnEnable()
    {
        _projectile.OnDestroy_TimerEnd += SpawnFx;
    }

    private void OnDisable()
    {
        _projectile.OnDestroy_TimerEnd -= SpawnFx;
    }

    private void SpawnFx()
    {
        var _myPosition = transform.position;
        var _startPosition = _myPosition + _offset;
        var _instance = Instantiate(_lineVfx, _startPosition, transform.rotation);
        _instance.Init(_myPosition);
    }
}
