using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilCamShaker : CameraShaker
{
    [SerializeField, ReadOnly] WeaponBehaviour _weapon = null;
    [SerializeField] float _velocityMultiplier = 0.2f;

    protected override void OnValidate()
    {
        base.OnValidate();

        if (_weapon is null)
            _weapon = GetComponent<WeaponBehaviour>();
    }

    private void OnEnable()
    {
        _weapon.onShoot += Shake;
    }

    private void OnDisable()
    {
        _weapon.onShoot -= Shake;
    }

    public override void Shake()
    {
        SetShape(CinemachineImpulseDefinition.ImpulseShapes.Recoil);
        var _velocity = -_weapon.transform.right * _velocityMultiplier;
        _impulseSource.GenerateImpulse(_velocity);
    }
}
