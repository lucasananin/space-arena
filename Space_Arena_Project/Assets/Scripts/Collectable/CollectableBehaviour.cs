using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    [SerializeField] CollectableSO _collectableSO = null;
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] Collider2D _collider = null;
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField, ReadOnly] CollectableAgent _agent = null;

    const float MIN_DISTANCE = 0.5f;

    private void FixedUpdate()
    {
        if (_agent is not null)
        {
            MoveToAgent();
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.TryGetComponent(out CollectableAgent _collectableAgent))
        {
            _agent = _collectableAgent;
            DisablePhysics();
        }
    }

    private void MoveToAgent()
    {
        var _myPosition = transform.position;
        var _targetPosition = _agent.transform.position;
        var _direction = (_targetPosition - _myPosition).normalized;
        var _velocity = _direction * _moveSpeed;
        _rb.velocity = _velocity;

        if (GeneralMethods.IsPointCloseToTarget(transform.position, _agent.transform.position, MIN_DISTANCE))
        {
            _collectableSO.Collect(_agent);
            Destroy(gameObject);
        }
    }

    private void DisablePhysics()
    {
        _collider.enabled = false;
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
