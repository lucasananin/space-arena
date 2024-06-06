using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamShaker : CameraShaker
{
    [SerializeField] CinemachineImpulseDefinition.ImpulseShapes _shape = CinemachineImpulseDefinition.ImpulseShapes.Bump;
    [SerializeField] Vector3 _velocity = Vector3.zero;
    [SerializeField] float _force = 1f;
    [SerializeField] bool _useVelocity = true;

    public override void Shake()
    {
        SetShape(_shape);

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
