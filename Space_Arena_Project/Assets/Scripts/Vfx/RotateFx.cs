using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFx : MonoBehaviour
{
    [SerializeField] Transform _transform = null;
    [SerializeField] float _twistsPerSecond = 1f;
    [SerializeField] bool _slowDown = false;
    [SerializeField, ReadOnly] float _speed = 0f;

    private void Awake()
    {
        _speed = GetDegreesPerSecond();
    }

    private void Update()
    {
        // Fazer com que os twists sejam relativos a velocidade do projetil.
        var _euler = Vector3.forward * _speed * Time.deltaTime;
        _transform.Rotate(_euler);

        if (_slowDown)
        {
            _speed -= GetDegreesPerSecond() * Time.deltaTime;
        }
    }

    private float GetDegreesPerSecond()
    {
        return _twistsPerSecond * 360f;
    }
}
