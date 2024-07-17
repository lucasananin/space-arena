using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashVfx : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weapon = null;
    [SerializeField] ParticleSystem _ps = null;

    private void OnValidate()
    {
        SetReferences();
    }

    private void OnEnable()
    {
        _weapon.OnShoot += Play;
    }

    private void OnDisable()
    {
        _weapon.OnShoot -= Play;
    }

    private void Play()
    {
        _ps.Play();
    }

    //[Button]
    private void SetReferences()
    {
        if (_weapon is null)
            _weapon = GetComponentInParent<WeaponBehaviour>(true);
    }
}
