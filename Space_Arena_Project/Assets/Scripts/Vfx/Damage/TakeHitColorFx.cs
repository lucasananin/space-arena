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
    [SerializeField, ReadOnly] float _timer = 0f;
    //[SerializeField, ReadOnly] bool _isOn = false;
    //private WaitForSeconds _waitForSeconds = null;

    private void OnValidate()
    {
        if (_health is null)
            _health = GetComponent<HealthBehaviour>();
    }

    private void Awake()
    {
        //_waitForSeconds = new WaitForSeconds(_duration);
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

    private void Update()
    {
        if (_timer >= _duration) return;

        _timer += Time.deltaTime;

        if (_timer >= _duration)
        {
            ResetMaterials();
        }
        //else
        //{
        //    if (_isOn)
        //        SetMaterials(_hitMaterial);
        //    else
        //        ResetMaterials();

        //    _isOn = !_isOn;
        //}
    }

    private void _health_OnDamageTaken()
    {
        _timer = 0f;
        SetMaterials(_hitMaterial);
        //_isOn = true;
        //StartCoroutine(ChangeMaterial_routine());
    }

    //private IEnumerator ChangeMaterial_routine()
    //{
    //    SetMaterials(_hitMaterial);
    //    yield return _waitForSeconds;
    //    ResetMaterials();
    //}

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
