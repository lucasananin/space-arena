using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotateFx : MonoBehaviour
{
    [SerializeField] Transform _transform = null;
    [SerializeField] float _twistsPerSecond = 1f;


    private void Update()
    {
        var _euler = GetDegreesPerSecond() * Time.deltaTime * Vector3.forward;
        _transform.Rotate(_euler);
    }

    private float GetDegreesPerSecond()
    {
        return _twistsPerSecond * 360f;
    }
}
