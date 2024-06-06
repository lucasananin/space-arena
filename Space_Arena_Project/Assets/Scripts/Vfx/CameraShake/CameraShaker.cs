using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public abstract class CameraShaker : MonoBehaviour
{
    [SerializeField, ReadOnly] protected CinemachineImpulseSource _impulseSource = null;

    protected virtual void OnValidate()
    {
        if (_impulseSource is null)
            _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void SetShape(CinemachineImpulseDefinition.ImpulseShapes _shape)
    {
        _impulseSource.m_ImpulseDefinition.m_ImpulseShape = _shape;
    }

    [Button]
    public abstract void Shake();
}
