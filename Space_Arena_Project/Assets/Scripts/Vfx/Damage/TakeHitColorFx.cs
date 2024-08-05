using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHitColorFx : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] _renderers = null;
    [SerializeField] HealthBehaviour _health = null;
    [SerializeField] Material _hitMaterial = null;
    [SerializeField] float _duration = 0.06f;
    [SerializeField, ReadOnly] List<Material> _defaultMaterials = null;

    private WaitForSeconds _waitForSeconds = null;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_duration);
        SetDefaults();
    }

    private void OnEnable()
    {
        _health.OnDamageTaken += _health_OnDamageTaken;
    }

    private void OnDisable()
    {
        _health.OnDamageTaken -= _health_OnDamageTaken;
    }

    private void _health_OnDamageTaken()
    {
        StartCoroutine(ChangeMaterial_routine());
    }

    private IEnumerator ChangeMaterial_routine()
    {
        SetMaterials(_hitMaterial);
        yield return _waitForSeconds;
        ResetMaterials();
    }

    private void SetMaterials(Material _mat)
    {
        int _count = _renderers.Length;

        for (int i = 0; i < _count; i++)
            _renderers[i].sharedMaterial = _mat;
    }

    private void ResetMaterials()
    {
        int _count = _renderers.Length;

        for (int i = 0; i < _count; i++)
            _renderers[i].sharedMaterial = _defaultMaterials[i];
    }

    private void SetDefaults()
    {
        int _count = _renderers.Length;

        for (int i = 0; i < _count; i++)
            _defaultMaterials.Add(_renderers[i].sharedMaterial);
    }
}
