using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecelerationBehavior : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] AnimationCurve _curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] float _multiplier = 1f;
    [SerializeField, ReadOnly] float _time = 0f;

    private void OnValidate()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (CanDeaccelerate())
        {
            Deaccelerate();
        }
    }

    private void Deaccelerate()
    {
        _time += Time.fixedDeltaTime * _multiplier;
        float _curveValue = _curve.Evaluate(_time);
        Vector2 _newVelocity = Vector2.Lerp(_rb.velocity, Vector2.zero, _curveValue);
        _rb.velocity = _newVelocity;
    }

    private bool CanDeaccelerate()
    {
        return !_rb.isKinematic && _rb.velocity != Vector2.zero;
    }
}
