using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PExplosionCamShaker : CameraShaker
{
    [SerializeField, ReadOnly] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] float _velocityMultiplier = 1f;

    protected override void OnValidate()
    {
        base.OnValidate();

        if (_projectileBehaviour is null)
            _projectileBehaviour = GetComponent<ProjectileBehaviour>();
    }

    private void OnEnable()
    {
        _projectileBehaviour.OnExplode += Shake;
    }

    private void OnDisable()
    {
        _projectileBehaviour.OnExplode -= Shake;
    }

    public override void Shake()
    {
        SetShape(Cinemachine.CinemachineImpulseDefinition.ImpulseShapes.Explosion);
        var _velocity = GeneralMethods.GetRandomDirection() * _velocityMultiplier;
        _impulseSource.GenerateImpulse(_velocity);
    }
}
