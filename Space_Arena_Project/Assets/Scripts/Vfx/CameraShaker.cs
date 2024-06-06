using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraShaker : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource _impulseSource = null;
    [SerializeField] CinemachineImpulseDefinition.ImpulseShapes _shape = CinemachineImpulseDefinition.ImpulseShapes.Bump;
    [SerializeField] Vector3 _velocity = Vector3.zero;
    [SerializeField] float _force = 1f;
    [SerializeField] bool _useVelocity = true;

    private void OnValidate()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    [Button]
    private void Shake()
    {
        _impulseSource.m_ImpulseDefinition.m_ImpulseShape = _shape;

        if (_useVelocity)
        {
            _impulseSource.GenerateImpulse(_velocity);
        }
        else
        {
            _impulseSource.GenerateImpulse(_force);
        }
    }
}
