using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaPMod : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] GameObject _damageAreaPrefab = null;
    [SerializeField] bool _onHit = true;
    [SerializeField] bool _onDestroy = true;

    private void OnEnable()
    {
        _projectileBehaviour.onRaycastHit += _projectileBehaviour_onRaycastHit;
        _projectileBehaviour.OnDestroyTimerEnd += _projectileBehaviour_OnDestroyTimerEnd;
    }

    private void OnDisable()
    {
        _projectileBehaviour.onRaycastHit -= _projectileBehaviour_onRaycastHit;
        _projectileBehaviour.OnDestroyTimerEnd -= _projectileBehaviour_OnDestroyTimerEnd;
    }

    private void _projectileBehaviour_onRaycastHit(RaycastHit2D _hit)
    {
        if (!_onHit) return;
        Instantiate(_damageAreaPrefab, _hit.point, Quaternion.identity);
    }

    private void _projectileBehaviour_OnDestroyTimerEnd()
    {
        if (!_onDestroy) return;
        Instantiate(_damageAreaPrefab, transform.position, Quaternion.identity);
    }
}
