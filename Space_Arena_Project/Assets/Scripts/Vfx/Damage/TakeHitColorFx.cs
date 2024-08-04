using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHitColorFx : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] _renderers = null;
    [SerializeField] HealthBehaviour _health = null;
    [SerializeField] Material _hitMaterial = null;
    [SerializeField] float _duration = 0.1f;
    [SerializeField, ReadOnly] List<Material> _defaultMaterials = null;
    [SerializeField, ReadOnly] float _timer = 0f;
    [SerializeField, ReadOnly] bool _isHit = false;

    private void Awake()
    {
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

    private void LateUpdate()
    {
        //if (_timer > _duration) return;
        return;
        _timer += Time.deltaTime * (1f / _duration);

        if (_timer > _duration && _isHit)
        {
            _isHit = false;
            ResetMaterials();
            Debug.Log($"reset");
        }
    }

    private void _health_OnDamageTaken()
    {
        StartCoroutine(ChangeMaterial_routine());
        return;
        _timer = 0f;
        _isHit = true;
        SetMaterials(_hitMaterial);
        Debug.Log($"hit");
    }

    private IEnumerator ChangeMaterial_routine()
    {
        SetMaterials(_hitMaterial);
        yield return new WaitForSeconds(_duration);
        ResetMaterials();
    }

    private void SetMaterials(Material _mat)
    {
        int _count = _renderers.Length;

        for (int i = 0; i < _count; i++)
            _renderers[i].material = _mat;
    }

    private void ResetMaterials()
    {
        int _count = _renderers.Length;

        for (int i = 0; i < _count; i++)
            _renderers[i].material = _defaultMaterials[i];
    }

    private void SetDefaults()
    {
        int _count = _renderers.Length;

        for (int i = 0; i < _count; i++)
            _defaultMaterials.Add(_renderers[i].material);
    }
}
