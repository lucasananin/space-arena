using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFx : MonoBehaviour
{
    [SerializeField] Transform _transform = null;
    [SerializeField] float _multiplier = 1f;
    [SerializeField] float _slowDownMultiplier = 1f;

    private void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        _multiplier -= Time.deltaTime * _slowDownMultiplier;
        var _euler = Vector3.forward * _multiplier;
        _transform.Rotate(_euler);
    }
}
