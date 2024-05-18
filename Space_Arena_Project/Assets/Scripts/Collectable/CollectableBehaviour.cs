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

    //[SerializeField] AnimationCurve _curve = null;
    //[SerializeField] float _deacceleration = 1f;
    //[SerializeField, ReadOnly] float _accelerationTime = 0f;

    const float MIN_DISTANCE = 0.5f;

    private void FixedUpdate()
    {
        if (_agent is not null)
        {
            MoveToAgent();
        }

        //CheckAcceleration();
    }

    //private void CheckAcceleration()
    //{
    //    if (_rb.isKinematic || _rb.velocity == Vector2.zero) return;

    //    _accelerationTime += Time.fixedDeltaTime * _deacceleration;
    //    float _curveValue = _curve.Evaluate(_accelerationTime);
    //    Vector2 _newVelocity = Vector2.Lerp(_rb.velocity, Vector2.zero, _curveValue);
    //    _rb.velocity = _newVelocity;
    //}

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
